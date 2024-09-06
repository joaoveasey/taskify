using Microsoft.AspNetCore.Mvc;

namespace Taskify.API.Controllers
{
    [ApiController]
    [Route("api/task")]
    public class TaskController : ControllerBase
    {
        [HttpGet]
        public void GetAllTasks() 
        {
            
        }
    }
}
