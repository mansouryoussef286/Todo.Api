using System;
using System.Security.Claims;
using Todo.Api.Domain.DTOs;
using Todo.Api.Domain.Enums;
using Todo.Api.Domain.Interfaces.IRepositories;

namespace Todo.Api.Domain.Services
{
    public class TasksService
    {
        private readonly ITasksRepository _TasksRepository;

        public TasksService(ITasksRepository taskRepository)
        {
            _TasksRepository = taskRepository;
        }

        public async Task<List<TodoTaskDTO>> GetTodoTasks()
        {
            return _TasksRepository.GetTodoTasks();
        }

        public async Task<TodoTaskDTO?> GetTodoTaskById(int todoTaskId)
        {
            return _TasksRepository.GetTodoTaskById(todoTaskId);
        }

        public async Task CreateTodoTask(CreateTodoTaskDTO todoTask, UserDTO user)
        {
            var newtoDoTask = new TodoTaskDTO()
            {
                Title = todoTask.Title,
                Description = todoTask.Description,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                Status = (int)TodoStatus.NotCompleted,
                UserId = user.Id,
            };

            _TasksRepository.CreateTodoTask(newtoDoTask);
        }

        public async Task<bool> UpdateTodoTask(TodoTaskDTO todoTask)
        {
            todoTask.UpdatedAt = DateTime.Now;
            var isUpdated = await _TasksRepository.UpdateTodoTask(todoTask);
            if (isUpdated)
            {
                var oldTask = GetTodoTaskById(todoTask.Id);
                return true;
            }
            return false;
        }

        public async Task DeleteTodoTask(int todoTaskId)
        {
            _TasksRepository.DeleteTodoTask(todoTaskId);
        }
    }
}
