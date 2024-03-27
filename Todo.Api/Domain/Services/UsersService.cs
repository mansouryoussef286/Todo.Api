using Todo.Api.Domain.DTOs;
using Todo.Api.Domain.Interfaces.IRepositories;

namespace Todo.Api.Domain.Services
{
    public class UsersService
    {
        private readonly IUsersRepository _UsersRepository;

        public UsersService(IUsersRepository userRepository)
        {
            _UsersRepository = userRepository;
        }

        public async Task<List<UserDTO>> GetUsers()
        {
            return _UsersRepository.GetUsers();
        }

        public async Task<UserDTO?> GetUserById(int UserId)
        {
            return await _UsersRepository.GetUserById(UserId);
        }

        public async Task CreateUser(UserDTO User)
        {
            _UsersRepository.CreateUser(User);
        }

        public async Task<bool> UpdateUser(UserDTO User)
        {
            var isUpdated = await _UsersRepository.UpdateUser(User);
            if (isUpdated)
            {
                var oldUser = GetUserById(User.Id);
                return true;
            }
            return false;
        }

        public async Task DeleteUser(int UserId)
        {
            _UsersRepository.DeleteUser(UserId);
        }
    }
}
