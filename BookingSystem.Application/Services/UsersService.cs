using BookingSystem.Application.Interfaces;
using BookingSystem.Domain.DTOs.Users;
using BookingSystem.Domain.Entities;
using Org.BouncyCastle.Crypto.Generators;


namespace BookingSystem.Application.Services
{
    public class UsersService
    {
        private readonly IUsersRepository _usersRepository;
        public UsersService(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        public async Task<UserResponse> AddUserAsync(User users)
        {
            var newUser = new User(users.Name, users.Email, users.Password);
            var result = await _usersRepository.AddUserAsync(newUser);
            await _usersRepository.SaveChangesAsync();
            return result;
        }
    }
}
