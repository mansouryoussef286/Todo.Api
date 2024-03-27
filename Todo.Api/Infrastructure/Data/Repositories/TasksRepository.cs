using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Todo.Api.Domain.DTOs;
using Todo.Api.Domain.Interfaces.IRepositories;
using Todo.Api.Infrastructure.Data.DbContexts;
using Todo.Api.Infrastructure.Data.Models;

namespace Todo.Api.Infrastructure.Data.Repositories
{
    public class TasksRepository : ITasksRepository
    {
        private readonly TodoDbContext _context;

        public TasksRepository(TodoDbContext context)
        {
            _context = context;
        }

        private TodoTaskDTO MapEntityToDTO(TodoTask todotask)
        {
            return new TodoTaskDTO
            {
                Id = todotask.Id,
                Title = todotask.Title,
                Description = todotask.Description,
                Status = todotask.Status,
                UpdatedAt = todotask.UpdatedAt,
                CreatedAt = todotask.CreatedAt,
                UserId = todotask.UserId
            };
        }

        private TodoTask MapDTOToEntity(TodoTaskDTO todotask)
        {
            return new TodoTask
            {
                Title = todotask.Title,
                Description = todotask.Description,
                Status = todotask.Status,
                UpdatedAt = todotask.UpdatedAt,
                CreatedAt = todotask.CreatedAt,
                UserId = todotask.UserId
            };
        }


        public async Task<List<TodoTaskDTO>> GetTodoTasks(int? userId = null)
        {
            List<TodoTask> todotasks;
            if (userId != 0 && userId != null)
                todotasks = await _context.Tasks.Where(t=>t.UserId == userId).ToListAsync();
            else
                todotasks = await _context.Tasks.ToListAsync();
            return todotasks.Select(MapEntityToDTO).ToList();
        }

        public TodoTaskDTO? GetTodoTaskById(int todoTaskId)
        {
            var todotask = _context.Tasks.Find(todoTaskId);
            return todotask != null ? MapEntityToDTO(todotask) : null;
        }

        public async Task<TodoTaskDTO> CreateTodoTask(TodoTaskDTO newtodotask)
        {
            var todotask = MapDTOToEntity(newtodotask);
            await _context.Tasks.AddAsync(todotask);
            await _context.SaveChangesAsync();
            return MapEntityToDTO(todotask);
        }


        public async Task<bool> UpdateTodoTask(TodoTaskDTO todoTask)
        {
            var existingProduct = await _context.Tasks.FirstOrDefaultAsync(t => t.Id == todoTask.Id);

            if (existingProduct != null)
            {
                //existingProduct.Name = todotask.Name;
                //existingProduct.Description = todotask.Description;
                //existingProduct.Price = todotask.Price;
                //existingProduct.Quantity = todotask.Quantity;
                //existingProduct.UpdatedAt = todotask.UpdatedAt;

                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public void DeleteTodoTask(int todoTaskId)
        {
            var todotaskToDelete = _context.Tasks.Find(todoTaskId);

            if (todotaskToDelete != null)
            {
                _context.Tasks.Remove(todotaskToDelete);
                _context.SaveChanges();
            }
        }
    }
}
