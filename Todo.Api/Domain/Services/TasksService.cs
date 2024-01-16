using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public List<TodoTaskDTO> GetTodoTasks()
        {
            return _TasksRepository.GetTodoTasks();
        }

        public TodoTaskDTO? GetTodoTaskById(int todoTaskId)
        {
            return _TasksRepository.GetTodoTaskById(todoTaskId);
        }

        public void CreateTodoTask(TodoTaskDTO todoTask)
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

        public void DeleteTodoTask(int todoTaskId)
        {
            _TasksRepository.DeleteTodoTask(todoTaskId);
        }
    }
}
