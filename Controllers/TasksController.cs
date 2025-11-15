
using ASP.NET_Core_REST_API.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace ASP.NET_Core_REST_API
{
    [ApiController]
    [Route("api/[controller]")]
    public class TasksController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TasksController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskDTO>>> GetAllTasks()
        {
            var tasks = await _taskService.GetAllTasksAsync();
            return Ok(tasks);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TaskDTO>> GetTask(int id)
        {
            var task = await _taskService.GetTaskByIdAsync(id);

            if(task == null)
            {
                return NotFound(new { message = $"Task with ID: {id} not found!" });
            }

            return Ok(task);
        }

        [HttpPost]
        public async Task<ActionResult<TaskDTO>> CreateTask(CreateTaskDTO dto)
        {
            var created = await _taskService.CreateTaskAsync(dto);
            return CreatedAtAction(nameof(GetTask), new {id = created.Id}, created);
        }
        
        [HttpPut("{id}")]
        public async Task<ActionResult<TaskDTO>> UpdateTask(int id, UpdateTaskDTO dto)
        {
            var updated =  await _taskService.UpdateTaskAsync(id, dto);

            if(updated == null)
            {
                return NotFound(new { message = $"Task with ID: {id} not found!" });
            }

            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTask(int id)
        {
            var deleted = await _taskService.DeleteTaskAsync(id);

            if(!deleted)
            {
                return NotFound(new { message = $"Task with ID: {id} not found!" });
            }

            return NoContent();
        }

    }
}