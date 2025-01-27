using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Register.Data;
using Register.Models;
using Register.Models.Entities;

namespace Register.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        [HttpPost]
        public IActionResult Login(LoginDto loginDto)
        {
            if (loginDto == null)
            {
                return BadRequest(new { success = false, message = "Invalid login data" });
            }

            var user = _context.Logins.FirstOrDefault(u => u.Email == loginDto.Email);
            if (user == null)
            {
                return NotFound(new { success = false, message = "Invalid email" });
            }

            if (user.Password != loginDto.Password)
            {
                return BadRequest(new { success = false, message = "Invalid password" });
            }

            return Ok(new { success = true, message = "Login success", user });
        }

    }
}
