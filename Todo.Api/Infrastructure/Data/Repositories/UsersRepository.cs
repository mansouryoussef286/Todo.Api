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
    public class UsersRepository : IUsersRepository
    {
        private readonly TodoDbContext _context;

        public UsersRepository(TodoDbContext context)
        {
            _context = context;
        }

        private UserDTO MapEntityToDTO(User user)
        {
            return new UserDTO
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                ProfilePicPath = user.ProfilePicPath
            };
        }

        private User MapDTOToEntity(UserDTO user)
        {
            return new User
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                ProfilePicPath = user.ProfilePicPath
            };
        }


        public List<UserDTO> GetUsers()
        {
            var users = _context.Users.ToList();
            return users.Select(MapEntityToDTO).ToList();
        }

        public async Task<UserDTO?> GetUserById(int userId)
        {
            var user = await _context.Users.FindAsync(userId);
            return user != null ? MapEntityToDTO(user) : null;
        }

        public async Task<UserDTO?> GetUserByEmail(string email)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            return user != null ? MapEntityToDTO(user) : null;
        }

        public UserDTO CreateUser(UserDTO newuser)
        {
            var user = MapDTOToEntity(newuser);
            _context.Users.Add(user);
            _context.SaveChanges();
            return MapEntityToDTO(user);
        }


        public async Task<bool> UpdateUser(UserDTO user)
        {
            var existingProduct = await _context.Users.FirstOrDefaultAsync(t => t.Id == user.Id);

            if (existingProduct != null)
            {
                //existingProduct.Name = user.Name;
                //existingProduct.Description = user.Description;
                //existingProduct.Price = user.Price;
                //existingProduct.Quantity = user.Quantity;
                //existingProduct.UpdatedAt = user.UpdatedAt;

                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public void DeleteUser(int userId)
        {
            var userToDelete = _context.Users.Find(userId);

            if (userToDelete != null)
            {
                _context.Users.Remove(userToDelete);
                _context.SaveChanges();
            }
        }
    }
}
