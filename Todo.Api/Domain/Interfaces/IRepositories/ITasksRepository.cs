﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Todo.Api.Domain.DTOs;

namespace Todo.Api.Domain.Interfaces.IRepositories
{
    public interface ITasksRepository
    {
        List<TodoTaskDTO> GetTodoTasks();

        TodoTaskDTO? GetTodoTaskById(int todoTaskId);

        Task<bool> UpdateTodoTask(TodoTaskDTO todoTask);
        Task<TodoTaskDTO> CreateTodoTask(TodoTaskDTO todoTask);

        void DeleteTodoTask(int todoTaskId);
    }
}
