﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
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

        [HttpPut]
        [Route("StartTask/{taskId:int}")]
        public IActionResult StartTask(int taskId)
        {
            try
            {
                _dbContext.Database.ExecuteSqlRaw("EXEC UpdateTaskStatusToInProgress @TaskID",new SqlParameter("@TaskID", taskId));
                return Ok(new { status = true, message = "Task Started" });
            }
            catch(Exception ex)
            {
                return StatusCode(500, new { status = false, message = "Error updating task", error = ex.Message });
            }
        }

        [HttpPut]
        [Route("CompleteTask/{taskId:int}")]
        public IActionResult CompleteTask(int taskId)
        {
            try 
            {
                _dbContext.Database.ExecuteSqlRaw("EXEC UpdateTaskStatusTOCompleted @TaskID",new SqlParameter("@TaskID", taskId));
                return Ok(new { status = true, message = "Task Completed" });
            }
            catch(Exception ex)
            {
                return StatusCode(500, new { status = false, message = "Error updating task", error = ex.Message });
            }
        }

        [HttpPut]
        [Route("UpdateTask/{taskId:int}")]
        public IActionResult UpdateTask(int taskId, UpdateTasksDto updateTasksDto)
        {
            if(taskId != updateTasksDto.Id)
            {
                return Ok(new { status = false, message = "Task ID mismatch" });
            }
            try
            {
                _dbContext.Database.ExecuteSqlRaw("EXEC UpdateTask @TaskId, @UserId, @TaskName, @TaskDescription, @TaskStatus, @TaskDeadline",
                    new SqlParameter("@TaskId", updateTasksDto.Id),
                    new SqlParameter("@UserId", updateTasksDto.UserId),
                    new SqlParameter("@TaskName", updateTasksDto.TaskName),
                    new SqlParameter("@TaskDescription", updateTasksDto.TaskDescription),
                    new SqlParameter("@TaskStatus", updateTasksDto.TaskStatus),
                    new SqlParameter("@TaskDeadline", updateTasksDto.TaskDeadline)
                );
                return Ok(new {status = true, message= "Task Updated Successfully" });
            }
            catch(Exception ex)
            {
                return StatusCode(500, new { status = false, message = "Error updating task", error = ex.Message });
            }
        }


        [HttpDelete]
        [Route("DeleteTask/{taskId:int}")]
        public IActionResult DeteteTask(int taskId)
        {
            try
            {
                var task = _dbContext.Tasks.Find(taskId);
                if (task == null) 
                {
                    return Ok(new { status = false, message = "Task Not Found" });
                }
                _dbContext.Tasks.Remove(task);
                _dbContext.SaveChanges();
                return Ok(new { status = true, message="Task deleted sucessfully"}); 
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { status = false, message = "Error deleting task", error = ex.Message });
            }
        }

        [HttpGet]
        public IActionResult GetAllTasks()
        {
            var allTasks = _dbContext.Tasks.ToList();

            if(allTasks == null || !allTasks.Any())
            {
                return Ok(new { status = false, message = "Tasks Not Found" });
            }
            return Ok(new { status = true, message = "successfully fetched tsks", data = allTasks });
        }

        [HttpGet]
        [Route("{UserId:guid}")]
        public IActionResult GetTaskById(Guid UserId)
        {
            var tasks = _dbContext.Tasks.Where(a => a.UserId == UserId);
            if (!tasks.Any())
            {
                return Ok(new { status = false, message = "Task Not Found" });
            }
            return Ok(new {status=true, message="successfylly fetched tasks", tasks});
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
            return Ok(new {status=true, message="Task added succesfully"});
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
