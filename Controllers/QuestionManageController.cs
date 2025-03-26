using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Register.Data;

namespace Register.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionManageController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IConfiguration configuration;

        public QuestionManageController(ApplicationDbContext _dbContext, IConfiguration configuration)
        {
            this._dbContext = _dbContext;
            this.configuration = configuration;
        }

        [HttpGet]
        [Route("{id:int}")]
        public IActionResult GetQuestionsRange(int id)
        {
            return Ok();
        }
    }
}
