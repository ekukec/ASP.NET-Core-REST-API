
using ASP.NET_Core_REST_API.DTOs;

namespace ASP.NET_Core_REST_API
{
    public interface ITaskService
    {
        Task<IEnumerable<TaskDTO>> GetAllTasksAsync();
        Task<TaskDTO?> GetTaskByIdAsync(Guid id);
        Task<TaskDTO> CreateTaskAsync(CreateTaskDTO dto);
        Task<TaskDTO?> UpdateTaskAsync(Guid id, UpdateTaskDTO dto);
        Task<bool> DeleteTaskAsync(Guid id);
    }
}