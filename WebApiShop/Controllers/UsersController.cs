using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using static WebApiShop.Controllers.UsersController;
using Entities;
using Repository;
using Services;
using DTOs;
using Microsoft.AspNetCore.Authorization;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApiShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
       public class UsersController : ControllerBase
    {
        private readonly ILogger<UsersController> _logger;
        private readonly IUserService _userService;
        private readonly JwtService _jwtService;
        public UsersController(ILogger<UsersController> logger, IUserService userService, JwtService jwtService)
        {
            _logger = logger;
            _userService = userService;
            _jwtService = jwtService;
        }
        [Authorize]
        [HttpGet("{id}")]

        public async Task<ActionResult<GetUserDTO>> Get(int id)
        {

            GetUserDTO user = await _userService.GetUserById(id);
            if (user == null)
                return NoContent();
            return Ok(user);
        }


        // POST api/<UsersController>
        [HttpPost]
        public async Task<ActionResult<GetUserDTO>> Post([FromBody] UserDTO user)
        {
            GetUserDTO _user = await _userService.AddUser(user);
            if (_user == null)
            {
                return BadRequest("סיסמא חלשה - נסה סיסמא שונה");
            }
            return CreatedAtAction(nameof(Get), new { id = _user.Id }, _user);

        }

        [HttpPost("Login")]
        public async Task<ActionResult<GetUserDTO>> Login([FromBody] LoginDTO loginDto)
        {
            GetUserDTO _user = await _userService.Login(loginDto);
            if (_user == null)
            {
                _logger.LogInformation("Login failed: UserName={UserName},Password={Password}", loginDto.UserName, loginDto.Password);
                return NoContent();
            }
            _logger.LogInformation("Login success: UserName={UserName},Password={Password}",
             loginDto.UserName, loginDto.Password);

            string token = _jwtService.GenerateToken(_user.Id, _user.UserName);
            Response.Cookies.Append("jwt", token, new CookieOptions
            {
                HttpOnly = true,
                //Secure = true,
                //SameSite = SameSiteMode.None,
                Secure = false,
                SameSite = SameSiteMode.Lax,
                Expires = DateTime.Now.AddHours(1)
            });
            return Ok(new
            {
                Token = token,
                User = _user
            });

        }
        // PUT api/<UsersController>/5

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] UserDTO userDto)
        {
            await _userService.UpdateUser(id, userDto);
            return Ok(userDto);
        }

      
    }
}









