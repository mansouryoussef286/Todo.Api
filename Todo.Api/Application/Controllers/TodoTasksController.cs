using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Todo.Api.Application.Attributes;
using Todo.Api.Domain.DTOs;
using Todo.Api.Domain.Services;
using Todo.Api.Infrastructure.Data.Models;

namespace Todo.Api.Application.Controllers
{
    [Route("api/todotasks")]
    [JWTAuthenticationFilter]
    [ExtractUserId]
    [ApiController]
    public class TodoTasksController : ControllerBase
    {
        private readonly TasksService _tasksService;
        public TodoTasksController(TasksService tasksService)
        {
            _tasksService = tasksService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoTaskDTO>>> GetTodoTasks()
        {
            return await _tasksService.GetTodoTasks();
        }

        [HttpGet("list")]
        public async Task<ActionResult<IEnumerable<TodoTaskDTO>>> GetTodoTasksByUser(int userId)
        {
            return await _tasksService.GetTodoTasksByUser(userId);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TodoTaskDTO?>> UpdateTodoTask(int id)
        {
            return await _tasksService.GetTodoTaskById(id);
        }

        [HttpPost]
        public async Task<TodoTaskDTO> CreateTodoTask(CreateTodoTaskDTO todoTask, int userId)
        {
            return await _tasksService.CreateTodoTask(todoTask, userId);
        }

        [HttpPut("{id}")]
        public async Task<bool> UpdateTodoTask(int id, TodoTaskDTO todoTask)
        {
            return await _tasksService.UpdateTodoTask(todoTask);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoTask(int id)
        {
            await _tasksService.DeleteTodoTask(id);
            return NoContent();
        }

        [HttpPut("{id}/toggle-status")]
        public async Task<bool> ToggleTodoTaskStatus(int id)
        {
            return await _tasksService.ToggleTodoTaskStatus(id);
        }
    }
}
