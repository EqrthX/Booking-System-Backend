using BookingSystem.Application.Interfaces;
using BookingSystem.Domain.DTOs.Users;
using BookingSystem.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using System.Text;

namespace BookingSystem.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUsersRepository _usersRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IConfiguration _configuration;

        public AuthService(
            IUsersRepository usersRepository,
            IPasswordHasher passwordHasher,
            IConfiguration configuration)
        {
            _usersRepository = usersRepository;
            _passwordHasher = passwordHasher;
            _configuration = configuration;
        }

        public async Task<LoginResponse> LoginAsync(LoginRequest request)
        {
            // 1. ตรวจสอบ input
            if (string.IsNullOrWhiteSpace(request.Email) || string.IsNullOrWhiteSpace(request.Password))
                throw new ArgumentException("กรุณากรอกอีเมลและรหัสผ่าน");

            // 2. ค้นหา user จากฐานข้อมูล
            var user = await _usersRepository.GetUserByEmailAsync(request.Email);
            if (user == null)
                throw new UnauthorizedAccessException("อีเมลหรือรหัสผ่านไม่ถูกต้อง");

            // 3. ตรวจสอบรหัสผ่าน
            if (!_passwordHasher.VerifyPassword(request.Password, user.Password))
                throw new UnauthorizedAccessException("อีเมลหรือรหัสผ่านไม่ถูกต้อง");

            // 4. สร้าง tokens
            var accessToken = GenerateAccessToken(user);
            var refreshToken = GenerateRefreshToken();
            var accessTokenExpiresAt = DateTime.UtcNow.AddMinutes(
                _configuration.GetValue<int>("Jwt:AccessTokenExpirationMinutes", 15));
            var refreshTokenExpiresAt = DateTime.UtcNow.AddDays(
                _configuration.GetValue<int>("Jwt:RefreshTokenExpirationDays", 7));

            // 5. บันทึก refresh token ลงฐานข้อมูล
            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiresAt = refreshTokenExpiresAt;
            user.LastLoginAt = DateTime.UtcNow;
            await _usersRepository.UpdateUserAsync(user);
            await _usersRepository.SaveChangesAsync();

            // 6. ส่ง response กลับ
            return new LoginResponse
            {
                UserId = user.Id,
                Name = user.Name,
                Email = user.Email,
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                AccessTokenExpiresAt = accessTokenExpiresAt,
                RefreshTokenExpiresAt = refreshTokenExpiresAt
            };
        }

        public async Task<LoginResponse> RefreshTokenAsync(RefreshTokenRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.RefreshToken))
                throw new ArgumentException("RefreshToken ไม่สามารถเป็นค่าว่างได้");

            // 1. ค้นหา user ที่มี refresh token นี้
            var user = await _usersRepository.GetUserByRefreshTokenAsync(request.RefreshToken);
            if (user == null)
                throw new UnauthorizedAccessException("RefreshToken ไม่ถูกต้อง");

            // 2. ตรวจสอบว่า refresh token ยังไม่หมดอายุ
            if (user.RefreshTokenExpiresAt < DateTime.UtcNow)
                throw new UnauthorizedAccessException("RefreshToken หมดอายุแล้ว");

            // 3. สร้าง access token ใหม่
            var accessToken = GenerateAccessToken(user);
            var refreshToken = GenerateRefreshToken();
            var accessTokenExpiresAt = DateTime.UtcNow.AddMinutes(
                _configuration.GetValue<int>("Jwt:AccessTokenExpirationMinutes", 15));
            var refreshTokenExpiresAt = DateTime.UtcNow.AddDays(
                _configuration.GetValue<int>("Jwt:RefreshTokenExpirationDays", 7));

            // 4. อัปเดต refresh token ใหม่
            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiresAt = refreshTokenExpiresAt;
            await _usersRepository.UpdateUserAsync(user);
            await _usersRepository.SaveChangesAsync();

            // 5. ส่ง response กลับ
            return new LoginResponse
            {
                UserId = user.Id,
                Name = user.Name,
                Email = user.Email,
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                AccessTokenExpiresAt = accessTokenExpiresAt,
                RefreshTokenExpiresAt = refreshTokenExpiresAt
            };
        }

        public string GenerateAccessToken(User user)
        {
            var secretKey = _configuration["Jwt:SecretKey"];
            var issuer = _configuration["Jwt:Issuer"];
            var audience = _configuration["Jwt:Audience"];
            var expirationMinutes = _configuration.GetValue<int>("Jwt:AccessTokenExpirationMinutes", 15);

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new List<System.Security.Claims.Claim>
            {
                new(System.Security.Claims.ClaimTypes.NameIdentifier, user.Id.ToString()),
                new(System.Security.Claims.ClaimTypes.Email, user.Email),
                new(System.Security.Claims.ClaimTypes.Name, user.Name),
                new("Role", user.Role.ToString())
            };

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(expirationMinutes),
                signingCredentials: credentials);

            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(token);
        }

        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[64];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }
    }
}
