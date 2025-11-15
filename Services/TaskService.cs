
using ASP.NET_Core_REST_API.DTOs;
using ASP.NET_Core_REST_API.Models;
using ASP.NET_Core_REST_API.Repositories;
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

        public async Task<TaskDTO?> GetTaskByIdAsync(int id)
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

        public async Task<TaskDTO?> UpdateTaskAsync(int id, UpdateTaskDTO dto)
        {
            var task = _mapper.Map<TaskItem>(dto);
            task.Id = id;
            var updated = await _repository.UpdateAsync(task);
            return _mapper.Map<TaskDTO>(updated);
        }

        public async Task<bool> DeleteTaskAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }
    }
}