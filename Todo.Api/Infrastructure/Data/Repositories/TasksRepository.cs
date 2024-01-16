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

        private TodoTaskDTO MapProductToDTO(TodoTask todotask)
        {
            return new TodoTaskDTO
            {

            };
        }

        private TodoTask MapDTOToProduct(TodoTaskDTO todotask)
        {
            return new TodoTask
            {

            };
        }


        public List<TodoTaskDTO> GetTodoTasks()
        {
            var todotasks = _context.Tasks.ToList();
            return todotasks.Select(MapProductToDTO).ToList();
        }

        public TodoTaskDTO? GetTodoTaskById(int todoTaskId)
        {
            var todotask = _context.Tasks.Find(todoTaskId);
            return todotask != null ? MapProductToDTO(todotask) : null;
        }

        public TodoTaskDTO CreateTodoTask(TodoTaskDTO newtodotask)
        {
            var todotask = MapDTOToProduct(newtodotask);
            _context.Tasks.Add(todotask);
            _context.SaveChanges();
            return MapProductToDTO(todotask);
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
