using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Register.Data;
using Register.Models;
using Register.Models.Entities;

namespace Register.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IConfiguration configuration;

        public TaskController(ApplicationDbContext _dbContext, IConfiguration configuration)
        {
            this._dbContext = _dbContext;
            this.configuration = configuration;
        }


        [HttpGet]
        public IActionResult GetAllTasks()
        {
            var allTasks = _dbContext.Tasks.ToList();

            if(allTasks == null || !allTasks.Any())
            {
                return Ok(Ok(new { status = false, message = "Tasks Not Found" }));
            }
            return Ok(new { status = true, message = "successfully fetched data", data = allTasks });
        }

        [HttpGet]
        [Route("{id:guid}")]
        public IActionResult GetTaskById(Guid id)
        {
            return Ok("Get Task By Id");
        }
        [HttpPost]
        public IActionResult AddTask(AddTasksDto addTasksDto)
        {
            var user = _dbContext.Signups.Find(addTasksDto.UserId);
            if(user == null)
            {
                return Ok(new { status = false, message = "Invalid User" });
            }
            var taskEntity = new Tasks()
            {
                UserId = addTasksDto.UserId,
                TaskName = addTasksDto.TaskName,
                TaskDescription = addTasksDto.TaskDescription,
                TaskStatus = addTasksDto.TaskStatus,
                TaskDeadline = addTasksDto.TaskDeadline
            };
            _dbContext.Tasks.Add(taskEntity);
            _dbContext.SaveChanges();
            return Ok("Add Task");
        }
        [HttpPut]
        public IActionResult UpdateTask()
        {
            return Ok("Update Task");
        }
        [HttpDelete]
        public IActionResult DeleteTask()
        {
            return Ok("Delete Task");
        }
    }
}
