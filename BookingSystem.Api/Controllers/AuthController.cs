using BookingSystem.Application.Interfaces;
using BookingSystem.Domain.DTOs;
using BookingSystem.Domain.DTOs.Users;
using BookingSystem.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace BookingSystem.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUsersRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IAuthService _authService;

        public AuthController(
            IUsersRepository userRepository,
            IPasswordHasher passwordHasher,
            IAuthService authService)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            try
            {
                var hashedPassword = _passwordHasher.HashPassword(request.Password);
                // 1. สร้าง User ใหม่ผ่าน Constructor
                var newUser = new User(
                    request.Name,
                    request.Email,
                    hashedPassword
                );

                // 2. สั่งบันทึกลง Database
                await _userRepository.AddUserAsync(newUser);
                await _userRepository.SaveChangesAsync(); // 👈 ต้อง SaveChanges เสมอ ข้อมูลถึงจะลง Database จริงๆ

                // 💡 ทริค: หลังจาก SaveChangesAsync() สำเร็จ Entity Framework จะดึง Id จาก Database 
                // มาใส่ในตัวแปร newUser ให้อัตโนมัติเลยครับ เราเลยสามารถเรียกใช้ newUser.Id ได้ทันที

                // 3. ส่ง Response กลับไปให้ Angular
                return Ok(new
                {
                    Message = "สมัครสมาชิกสำเร็จ",
                    UserId = newUser.Id
                });
            }
            catch (ArgumentException ex)
            {
                // ดักจับ Error กรณีส่งข้อมูลว่างๆ มา (ที่เราเขียนเช็คไว้ใน Constructor)
                return BadRequest(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                // ดักจับ Error กรณีอีเมลซ้ำ
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                // ดักจับ Error อื่นๆ เช่น Database พัง
                return StatusCode(500, "เกิดข้อผิดพลาดภายในระบบ: " + ex.Message);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            try
            {
                var response = await _authService.LoginAsync(request);
                return Ok(response);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "เกิดข้อผิดพลาดภายในระบบ: " + ex.Message });
            }
        }

        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequest request)
        {
            try
            {
                var response = await _authService.RefreshTokenAsync(request);
                return Ok(response);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "เกิดข้อผิดพลาดภายในระบบ: " + ex.Message });
            }
        }
    }
}