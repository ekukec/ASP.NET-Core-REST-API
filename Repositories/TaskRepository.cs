using ASP.NET_Core_REST_API.Models;
using ASP.NET_Core_REST_API.Data;
using Microsoft.EntityFrameworkCore;

namespace ASP.NET_Core_REST_API.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly AppDbContext _context;

        public TaskRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TaskItem>> GetAllAsync()
        {
            return await _context.Tasks.OrderByDescending(t => t.CreatedAt).ToListAsync();
        }

        public async Task<TaskItem?> GetByIdAsync(int id)
        {
            return await _context.Tasks.FindAsync(id);
        }

        public async Task<TaskItem> CreateAsync(TaskItem task)
        {
            task.CreatedAt = DateTime.Now;
            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();
            return task;
        }

        public async Task<TaskItem?> UpdateAsync(TaskItem task)
        {
            var existing = await _context.Tasks.FindAsync(task.Id);
            if (existing == null) return null;

            existing.Title = task.Title;
            existing.Description = task.Description;

            if(task.IsCompleted && !existing.IsCompleted)
            {
                existing.CompletedAt = DateTime.Now;
            }
            else if(!task.IsCompleted && existing.IsCompleted)
            {
                existing.CompletedAt = null;
            }

            existing.IsCompleted = task.IsCompleted;

            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if(task == null) return false;

            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Tasks.AnyAsync(t => t.Id == id);
        }
    }
}