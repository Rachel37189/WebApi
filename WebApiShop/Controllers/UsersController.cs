using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Entities;
using Repository;
using Services;

namespace WebApiShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserService _userService = new UserService();
        
        [HttpGet("{id}")]
        public ActionResult<User> Get(int id)
        {
            User user = _userService.GetUserById(id);
            if (user == null)
                return NotFound();
            return Ok(user);
        }
        
        [HttpPost]
        public ActionResult<User> Post([FromBody] User user)
        {
            User newUser = _userService.AddUser(user);
            if (newUser == null)
            {
                return BadRequest("סיסמא חלשה - נסה סיסמא שונה");
            }
            return CreatedAtAction(nameof(Get), new { id = newUser.Id }, newUser);
        }

        [HttpPost("Login")]
        public ActionResult<User> Login([FromBody] User user)
        {
            User loggedInUser = _userService.Login(user);
            if (loggedInUser == null)
                return Unauthorized();
            return Ok(loggedInUser);
        }
        
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] User user)
        {
            _userService.UpdateUser(id, user);
        }
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
