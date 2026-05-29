using BookingSystem.Domain.DTOs.Users;
using BookingSystem.Domain.Entities;

namespace BookingSystem.Application.Interfaces
{
    public interface IUsersRepository
    {
        Task<UserResponse> AddUserAsync(User users);
        Task SaveChangesAsync();
        Task<User> GetUserByEmailAsync(string email);
        Task<User> GetUserByIdAsync(int id);
        Task<User> GetUserByRefreshTokenAsync(string refreshToken);
        Task UpdateUserAsync(User user);
    }
}
