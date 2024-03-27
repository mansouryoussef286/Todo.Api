using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Todo.Api.Domain.DTOs;

namespace Todo.Api.Domain.Interfaces.IRepositories
{
    public interface IUsersRepository
    {
        List<UserDTO> GetUsers();
        Task<UserDTO?> GetUserById(int id);
        Task<UserDTO?> GetUserByEmail(string email);
        Task<bool> UpdateUser(UserDTO todoTask);
        UserDTO CreateUser(UserDTO todoTask);
        void DeleteUser(int todoTaskId);
    }
}
