using BookingSystem.Application.Interfaces;
using BCryptTool = BCrypt.Net.BCrypt;


namespace BookingSystem.Infrastructure.Repositories
{
    public class BcryptPasswordHasher : IPasswordHasher
    {
        public string HashPassword(string password)
        {
            return BCryptTool.HashPassword(password);
        }

        public bool VerifyPassword(string password, string passwordHash)
        {
            return BCryptTool.Verify(password, passwordHash);
        }
    }
}
