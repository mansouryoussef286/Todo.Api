using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Todo.Api.Domain.DTOs;
using Todo.Api.Domain.Services;

namespace Todo.Api.Application.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UsersService _usersService;
        public UsersController(UsersService usersService)
        {
            _usersService = usersService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetUsers()
        {
            return await _usersService.GetUsers();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDTO>> GetUser(int id)
        {
            return await _usersService.GetUserById(id);
        }

        [HttpPut("{id}")]
        public async Task<bool> UpdateUser(int id, UserDTO user)
        {
            return await _usersService.UpdateUser(user);
        }

        [HttpPost]
        public async Task<ActionResult<UserDTO>> CreateUser(UserDTO user)
        {
             await _usersService.CreateUser(user);
            return CreatedAtAction("GetUser", new { id = user.Id }, user);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
             await _usersService.DeleteUser(id);
            return NoContent();
        }
    }
}
