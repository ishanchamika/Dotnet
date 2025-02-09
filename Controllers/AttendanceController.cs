using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Register.Data;
using Register.Models;
using Register.Models.Entities;

namespace Register.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttendanceController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IConfiguration configuration;

        public AttendanceController(ApplicationDbContext _dbContext, IConfiguration configuration)
        {
            this._dbContext = _dbContext;
            this.configuration = configuration;
        }

        [HttpGet]
        public IActionResult GetAllAttendances()
        {
            var allAttendances = _dbContext.Attendances.ToList();
            if (allAttendances == null || !allAttendances.Any())
            {
                return NotFound("Attendances Not Found");
            }
            return Ok(allAttendances);
        }

        [HttpGet]
        [Route("{id:guid}")]
        public IActionResult GetAttendanceById(Guid id)
        {
            var attendance = _dbContext.Attendances.Find(id);
            if (attendance == null)
            {
                return NotFound("Attendance Not Found");
            }
            return Ok(attendance);
        }

        [HttpPost]
        public IActionResult AddAttendance(AddAttendanceDto addAttendanceDto)
        {
            var user = _dbContext.Signups.FirstOrDefault(u => u.Id == addAttendanceDto.UserId);
            if(user == null)
            {
                return Ok(new { status= false, message = "User Not Found" });
            }
            TimeZoneInfo sriLankaTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Sri Lanka Standard Time");
            DateTime currentTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, sriLankaTimeZone);

            var attendanceEntity = new Attendance()
            {
                UserId = addAttendanceDto.UserId,
                Date = currentTime.Date,
                CameTime = addAttendanceDto.CameTime ?? currentTime,
                LeftTime = addAttendanceDto.LeftTime ?? currentTime,
                IsUpdated = addAttendanceDto.IsUpdated,
                Reason = addAttendanceDto.Reason
            };
            _dbContext.Attendances.Add(attendanceEntity);
            _dbContext.SaveChanges();
            return Ok(new {status = true, message = "Attendance Added Successfully" });
        }
    }
}
