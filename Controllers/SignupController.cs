using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
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
        private readonly IConfiguration configuration;

        public SignupController(ApplicationDbContext _dbContext, IConfiguration configuration)
        {
            this._dbContext = _dbContext;
            this.configuration = configuration;
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
        [Authorize]
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


        [HttpPost("login")]
        public IActionResult Login(LoginDto loginDto)
        {
            if (loginDto == null)
            {
                return Ok(new { status = false, message = "Invalid login data" });
            }

            var user = _dbContext.Signups.FirstOrDefault(u => u.Email == loginDto.Email);

            if (user == null)
            {
                return Ok(new { status = false, message = "Invalid email" });
            }

            if (user.Password != loginDto.Password)
            {
                return Ok(new { status = false, message = "Invalid password" });
            }
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, configuration["Jwt:Subject"]),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("UserId", user.Id.ToString()),
                new Claim("Email", user.Email.ToString()),
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(configuration["Jwt:Issuer"], configuration["Jwt:Audience"], claims, expires: DateTime.UtcNow.AddMinutes(10),signingCredentials: signIn);
            string tokenValue = new JwtSecurityTokenHandler().WriteToken(token);

            return Ok(new { status = true, message = "Login successful", Token = tokenValue ,data = user });
        }


        [Authorize] // Ensures the endpoint requires authentication
        [HttpGet("profile")]
        public IActionResult GetUserProfile()
        {
            var userId = User.FindFirst("UserId")?.Value;  // Get UserId from token
            var email = User.FindFirst("Email")?.Value;    // Get Email from token

            if (userId == null || email == null)
            {
                return Unauthorized(new { status = false, message = "Invalid token" });
            }

            return Ok(new { status = true, message = "User data retrieved", userId, email });
        }

    }
}
