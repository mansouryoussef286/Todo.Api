using System;
using Todo.Api.Domain.DTOs;
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

        public async Task<List<TodoTaskDTO>>  GetTodoTasks()
        {
            return _TasksRepository.GetTodoTasks();
        }

        public async Task<TodoTaskDTO?>  GetTodoTaskById(int todoTaskId)
        {
            return _TasksRepository.GetTodoTaskById(todoTaskId);
        }

        public async Task  CreateTodoTask(TodoTaskDTO todoTask)
        {
            todoTask.CreatedAt = DateTime.Now;
            todoTask.UpdatedAt = DateTime.Now;
            _TasksRepository.CreateTodoTask(todoTask);
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
