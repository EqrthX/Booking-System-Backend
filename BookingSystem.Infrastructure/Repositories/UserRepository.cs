using BookingSystem.Application.Interfaces;
using BookingSystem.Domain.DTOs.Users;
using BookingSystem.Domain.Entities;
using BookingSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BookingSystem.Infrastructure.Repositories
{
    public class UserRepository : IUsersRepository
    {
        private readonly ApplicationDbContext _context;
        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<UserResponse> AddUserAsync(User users)
        {
            var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == users.Email);
            if (existingUser == null)
            {
                var result = await _context.Users.AddAsync(users);
                return new UserResponse
                {
                    Id = result.Entity.Id,
                    Name = result.Entity.Name,
                    Email = result.Entity.Email
                };
            }
            throw new InvalidOperationException("มีบัญชีผู้ใช้งานนี้แล้ว");
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<User> GetUserByRefreshTokenAsync(string refreshToken)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.RefreshToken == refreshToken);
        }

        public async Task UpdateUserAsync(User user)
        {
            _context.Users.Update(user);
        }
    }
}
