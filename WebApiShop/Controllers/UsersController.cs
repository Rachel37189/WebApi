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

        private readonly IUserService _userService ;
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        // GET api/<UsersController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> Get(int id)
        {
           
            User user= await _userService.GetUserById(id);
            if (user == null)
                   return NotFound();
            return Ok(user);
        }
        // POST api/<UsersController>
        [HttpPost]
        public async Task<ActionResult<User>> Post([FromBody] User user)
        {
           User _user = await _userService.AddUser(user);
            if (_user == null)
            {
                return BadRequest("סיסמא חלשה - נסה סיסמא שונה");
            }
            return CreatedAtAction(nameof(Get), new { id = user.Id }, user);

        }

        [HttpPost("Login")]
        public async Task<ActionResult<User>> Login([FromBody] User user)
        {
           User _user = await _userService.Login(user);
            if (_user == null)
                return Unauthorized("Invalid email or password.") ;
            return Ok(_user);

        }
        // PUT api/<UsersController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] User user)
        {
            await _userService.UpdateUser(id, user);
            return NoContent();
        }

        // DELETE api/<UsersController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
