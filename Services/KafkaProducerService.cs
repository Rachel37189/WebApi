using Confluent.Kafka;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Services
{
    public class KafkaProducerService : IKafkaProducerService, IDisposable
    {
        private readonly string _topic;
        private readonly ILogger<KafkaProducerService> _logger;

        // 1. הגדרת משתנה ברמת המחלקה שיחזיק את ה-Producer קבוע בזיכרון
        private readonly IProducer<Null, string> _producer;

        public KafkaProducerService(IConfiguration configuration, ILogger<KafkaProducerService> logger)
        {
            _topic = configuration["Kafka:Topic"];
            _logger = logger;

            // 2. יצירת ההגדרות פעם אחת בלבד בתוך ה-Constructor
            var bootstrapServers = configuration["Kafka:BootstrapServers"];
            var config = new ProducerConfig
            {
                BootstrapServers = bootstrapServers,
                MessageTimeoutMs = 5000,
                SocketTimeoutMs = 5000,

            };

            // 3. בניית ה-Producer פעם אחת בלבד בעליית השרות
            _producer = new ProducerBuilder<Null, string>(config).Build();
            _logger.LogInformation("Kafka Producer initialized successfully.");
        }

        public async Task ProduceAsync(string message)
        {
            try
            {
                _logger.LogInformation("Starting Kafka publish");

                var result = await _producer.ProduceAsync(
                    _topic,
                    new Message<Null, string>
                    {
                        Value = message
                    });

                _logger.LogInformation(
                    "Message sent to Kafka. Topic: {Topic}, Partition: {Partition}, Offset: {Offset}",
                    result.Topic,
                    result.Partition,
                    result.Offset);
            }
            catch (ProduceException<Null, string> ex)
            {
                _logger.LogError(ex, "Kafka publish failed");
                throw;
            }
        }
        // 5. שחרור מסודר של החיבור רק כשהאפליקציה כולה נסגרת
        public void Dispose()
        {
            try
            {
                _producer?.Flush(TimeSpan.FromSeconds(10));
                _producer?.Dispose();
                _logger.LogInformation("Kafka Producer disposed cleanly.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during Kafka Producer disposal");
            }
        }
    }
}
