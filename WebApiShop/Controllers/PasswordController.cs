using Entities;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace WebApiShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PasswordController : ControllerBase
    {
        private readonly PasswordService _passwordService = new PasswordService();

        [HttpPost]
        public ActionResult<int> Post([FromBody] string password)
        {
            PasswordEntity passwordEntity = _passwordService.CheckPasswordStrength(password);
            if (passwordEntity == null)
                return NoContent();

            return Ok(passwordEntity.Strength);
        }
    }
}
