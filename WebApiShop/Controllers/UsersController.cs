using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using static WebApiShop.Controllers.UsersController;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApiShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {

        string _filePath = "C:\\Users\\yyy\\Desktop\\h.w\\WEB API\\newUsers.txt";
     
        // GET: api/<UsersController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<UsersController>/5
        [HttpGet("{id}")]
        public ActionResult<User> Get(int id)
        {
           
            using (StreamReader _reader = System.IO.File.OpenText(_filePath))
            {
                string? currentUserInFile;
                while ((currentUserInFile = _reader.ReadLine()) != null)
                {
                    User? _user = JsonSerializer.Deserialize<User>(currentUserInFile);
                    if (_user!=null && _user.id == id)
                        return Ok(_user);
                }
            }
                   return NoContent();
        }
        // POST api/<UsersController>
        [HttpPost]
        public ActionResult<User> Post([FromBody] User user)
        {
            if (!ModelState.IsValid)
                return ValidationProblem(ModelState); // ← כאן

            int numberOfUsers = System.IO.File.ReadLines(_filePath).Count();
            user.id = numberOfUsers + 1;
            string userJson = JsonSerializer.Serialize(user);
            System.IO.File.AppendAllText(_filePath, userJson + Environment.NewLine);
            return CreatedAtAction(nameof(Get), new { id = user.id }, user);
        }

        [HttpPost("Login")]
        public ActionResult<User> Login([FromBody] User user)
        {
            if (!ModelState.IsValid)
                return ValidationProblem(ModelState);
            using (StreamReader reader = System.IO.File.OpenText(_filePath))
            {
                string? currentUserInFile;
                while ((currentUserInFile = reader.ReadLine()) != null)
                {
                    User? _info = JsonSerializer.Deserialize<User>(currentUserInFile);
                    if (_info!=null && _info.userName == user.userName && _info.passWord == user.passWord)
                        return Ok(_info);
                   }
            }
            return NoContent() ;

        }
        // PUT api/<UsersController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] User user)
        {
            string textToReplace = string.Empty;
            

            using (StreamReader reader = System.IO.File.OpenText(_filePath))
            {
                string? currentUserInFile;
                while ((currentUserInFile = reader.ReadLine()) != null)
                {

                    User? _info = JsonSerializer.Deserialize<User>(currentUserInFile);
                    if (_info!=null && _info.id == id)
                        textToReplace = currentUserInFile;
                }
            }

            if (textToReplace != string.Empty)
            {
                string _text = System.IO.File.ReadAllText(_filePath);
                _text = _text.Replace(textToReplace, JsonSerializer.Serialize(user));
                System.IO.File.WriteAllText(_filePath, _text);
            }
        }

        // DELETE api/<UsersController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
