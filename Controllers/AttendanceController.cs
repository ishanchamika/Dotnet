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
        public IActionResult AddAttendance([FromBody] AddAttendanceDto addAttendanceDto)
        {
            try
            {
                var user = _dbContext.Signups.FirstOrDefault(u => u.Id == addAttendanceDto.UserId);
                if (user == null)
                {
                    return Ok(new { status = false, message = "User Not Found" });
                }

                var existingAttendance = _dbContext.Attendances.FirstOrDefault(a => a.UserId == addAttendanceDto.UserId && a.Date == addAttendanceDto.Date);

                if (existingAttendance != null)
                {
                    if (existingAttendance.CameTime != null && existingAttendance.LeftTime != null)
                    {
                        return Ok(new { status = false, message = "Already marked your came and left attendance" });
                    }
                    else if (existingAttendance.CameTime != null && existingAttendance.LeftTime == null)
                    {
                        if(addAttendanceDto.LeftTime == null) 
                        {
                            return Ok(new {status = false, message = "Left time Should be required"});
                        }
                        existingAttendance.LeftTime = addAttendanceDto.LeftTime;
                        _dbContext.SaveChanges();
                        return Ok(new { status = true, message = "Attendance Updated Successfully(left)" });
                    }
                    else if (existingAttendance.CameTime == null && existingAttendance.LeftTime == null)
                    {
                        if(addAttendanceDto.CameTime == null)
                        {
                            return Ok(new { status = false, message = "Came time Should be required" });
                        }
                        existingAttendance.CameTime = addAttendanceDto.CameTime;
                        _dbContext.SaveChanges();
                        return Ok(new { status = true, message = "Attendance Updated Successfully(came)" });
                    }
                    return Ok(new { status = false, message = "Attendance Already marked for Today" });
                }
                else
                {
                    var attendanceEntity = new Attendance()
                    {
                        UserId = addAttendanceDto.UserId,
                        Date = addAttendanceDto.Date,
                        CameTime = addAttendanceDto.CameTime,
                        LeftTime = null,
                        IsUpdated = false,
                        Reason = null
                    };
                    _dbContext.Attendances.Add(attendanceEntity);
                    _dbContext.SaveChanges();
                    return Ok(new { status = true, message = "Attendance Marked Successfully" });
                }
            }
            catch (Exception error)
            {
                return Ok(new { status = false, message = "An error occurred", error = error.Message });
            }
        }
    }
}
