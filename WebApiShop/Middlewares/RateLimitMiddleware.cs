using System.Collections.Concurrent;

namespace WebApiShop.Middlewares
{
    public class RateLimitMiddleware
    {
        private readonly RequestDelegate _next;

        private static ConcurrentDictionary<string, (int Count, DateTime WindowStart)> store
            = new();

        private const int MAX_REQUESTS = 10;
        private static readonly TimeSpan WINDOW = TimeSpan.FromMinutes(1);

        public RateLimitMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var key = context.Connection.RemoteIpAddress?.ToString() ?? "unknown";
            var now = DateTime.UtcNow;

            // אם משתמש חדש → נכניס אותו
            var data = store.GetOrAdd(key, (1, now));

            // אם עבר חלון הזמן → מאפסים
            if (now - data.WindowStart > WINDOW)
            {
                store[key] = (1, now);
                await _next(context);
                return;
            }

            // אם עדיין בתוך החלון ויש מקום
            if (data.Count < MAX_REQUESTS)
            {
                store[key] = (data.Count + 1, data.WindowStart);
                await _next(context);
                return;
            }

            // ❌ חרג מהמגבלה
            context.Response.StatusCode = 429;
            context.Response.ContentType = "application/json";

            await context.Response.WriteAsync("{\"message\":\"Too many requests\"}");
        }
    }
}
