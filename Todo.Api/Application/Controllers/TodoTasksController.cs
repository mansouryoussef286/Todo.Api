using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Todo.Api.Domain.DTOs;
using Todo.Api.Domain.Services;
using Todo.Api.Infrastructure.Data.Models;

namespace Todo.Api.Application.Controllers
{
    [Route("api/todotasks")]
    [JWTAuthenticationFilter]
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
        public async Task<ActionResult<IEnumerable<TodoTaskDTO>>> GetTodoTasksByUser()
        {
            var email = (HttpContext.Items["User"] as ClaimsPrincipal).FindFirst("Id")?.Value;
            return await _tasksService.GetTodoTasks();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TodoTaskDTO>> UpdateTodoTask(int id)
        {
            return await _tasksService.GetTodoTaskById(id);
        }

        [HttpPut("{id}")]
        public async Task<bool> UpdateTodoTask(int id, TodoTaskDTO todoTask)
        {
            return await _tasksService.UpdateTodoTask(todoTask);
        }

        [HttpPost]
        public async Task<ActionResult<TodoTask>> todoTask(CreateTodoTaskDTO todoTask)
        {
            var user = (HttpContext.Items["User"] as UserDTO);
            await _tasksService.CreateTodoTask(todoTask, user);
            return Created();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoTask(int id)
        {
            await _tasksService.DeleteTodoTask(id);
            return NoContent();
        }
    }
}
