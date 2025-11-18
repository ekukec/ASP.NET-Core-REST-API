
using ASP.NET_Core_REST_API.DTOs;
using ASP.NET_Core_REST_API.Models;
using ASP.NET_Core_REST_API.Repositories;
using ASP.NET_Core_REST_API.Exceptions;
using AutoMapper;

namespace ASP.NET_Core_REST_API
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _repository;
        private readonly IMapper _mapper;

        public TaskService(ITaskRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TaskDTO>> GetAllTasksAsync()
        {
            var tasks = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<TaskDTO>>(tasks);
        }

        public async Task<TaskDTO?> GetTaskByIdAsync(Guid id)
        {
            var task = await _repository.GetByIdAsync(id);
            return _mapper.Map<TaskDTO>(task);
        }

        public async Task<TaskDTO> CreateTaskAsync(CreateTaskDTO dto)
        {
            var task = _mapper.Map<TaskItem>(dto);
            var created = await _repository.CreateAsync(task);
            return _mapper.Map<TaskDTO>(created);
        }

        public async Task<TaskDTO?> UpdateTaskAsync(Guid id, UpdateTaskDTO dto)
        {
            var task = _mapper.Map<TaskItem>(dto);
            task.Id = id;
            var updated = await _repository.UpdateAsync(task);
            return _mapper.Map<TaskDTO>(updated);
        }

        public async Task<bool> DeleteTaskAsync(Guid id)
        {
            var existingTask = await _repository.GetByIdAsync(id);

            if (existingTask != null && existingTask.IsCompleted)
            {
                throw new BadRequestException("Cannot delete completed tasks");
            }

            return await _repository.DeleteAsync(id);
        }
    }
}