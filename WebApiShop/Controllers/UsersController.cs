using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using static WebApiShop.Controllers.UsersController;
using Entities;
using Repository;
using Services;
using DTOs;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApiShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ILogger<UsersController> _logger;
        public UsersController(ILogger<UsersController> logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }
        IUserService _userService ;
        //public UsersController(IUserService userService)
        //{
        //    _userService = userService;
        //}

        

      
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDTO>> Get(int id)
        {
           
            UserDTO user= await _userService.GetUserById(id);
            if (user == null)
                   return NotFound();
            return Ok(user);
        }
       
        [HttpPost]
        public async Task<ActionResult<UserDTO>> Post([FromBody] User user)
        {
           UserDTO _user = await _userService.addUser(user);
            if (_user == null)
            {
                return BadRequest("סיסמא חלשה - נסה סיסמא שונה");
            }
            return CreatedAtAction(nameof(Get), new { id = user.Id }, user);

        }

        [HttpPost("Login")]
        public async Task<ActionResult<UserDTO>> Login([FromBody] User user)
        {
           UserDTO _user = await _userService.login(user);
            if (_user == null) {
                _logger.LogInformation("Login failed: UserName={UserName},Password={Password}", user.UserName,user.Password);
                return Unauthorized("Invalid email or password.") ;
            }
            _logger.LogInformation("Login success: UserName={UserName},Password={Password}",
             user.UserName, user.Password);
            return Ok(_user);

        }
        
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] User user)
        {
            await _userService.updateUser(id, user);
            return Ok(user);
        }

        
    }
}
