using BookingSystem.Domain.DTOs.Users;
using BookingSystem.Domain.Entities;

namespace BookingSystem.Application.Interfaces
{
    public interface IAuthService
    {
        Task<LoginResponse> LoginAsync(LoginRequest request);
        Task<LoginResponse> RefreshTokenAsync(RefreshTokenRequest request);
        string GenerateAccessToken(User user);
        string GenerateRefreshToken();
    }
}
