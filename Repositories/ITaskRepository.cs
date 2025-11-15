using ASP.NET_Core_REST_API.Models;

namespace ASP.NET_Core_REST_API.Repositories
{
    public interface ITaskRepository
    {
        Task<IEnumerable<TaskItem>> GetAllAsync();
        Task<TaskItem?> GetByIdAsync(int id);
        Task<TaskItem> CreateAsync(TaskItem item);
        Task<TaskItem?> UpdateAsync(TaskItem item);
        Task<bool> DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
    }
}