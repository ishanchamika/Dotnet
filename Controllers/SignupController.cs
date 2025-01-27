using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Register.Data;
using Register.Models;
using Register.Models.Entities;

namespace Register.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SignupController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        public SignupController(ApplicationDbContext _dbContext)
        {
            this._dbContext = _dbContext;
        }

        [HttpGet]
        public IActionResult GetAllUsers() 
        {
            var allUsers = _dbContext.Signups.ToList();

            if (allUsers == null || !allUsers.Any())
            {
                return NotFound("Users Not Found");
            }
            return Ok(allUsers);
        }

        [HttpGet]
        [Route("{id:guid}")]
        public IActionResult GetUserById(Guid id) 
        {
            var user = _dbContext.Signups.Find(id);
            if (user == null) 
            {
                return NotFound("User Not Found");
            }
            return Ok(user);
        }

        [HttpPost]
        public IActionResult AddUser(AddSignupDto addSignupDto)
        {
            var signupEntity = new Signup()
            {
                Name = addSignupDto.Name,
                Email = addSignupDto.Email,
                Password = addSignupDto.Password
            };

            _dbContext.Signups.Add(signupEntity);
            _dbContext.SaveChanges();
            return Ok(new { status = true, message = "User added successfully!", data = signupEntity });
        }

        [HttpPut]
        [Route("{id:guid}")]
        public IActionResult UpdateUser(Guid id, UpdateSignupDto updateSignupDto)
        {
            var user = _dbContext.Signups.Find(id);
            if(user is null)
            {
                return NotFound("User Not found");
            }
            user.Name = updateSignupDto.Name;
            user.Email = updateSignupDto.Email;
            user.Password = updateSignupDto.Password;
            _dbContext.SaveChanges();
            return Ok(user);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public IActionResult DeleteUser(Guid id) 
        {
            var user = _dbContext.Signups.Find(id);
            if(user is null )
            {
                return NotFound("User Not Found");
            }
            _dbContext.Signups.Remove(user);
            _dbContext.SaveChanges();
            return Ok(user);
        }
    }
}
