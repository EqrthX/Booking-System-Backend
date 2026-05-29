using BookingSystem.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookingSystem.Domain.Entities
{
    [Table("Users")]
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "กรุณาระบุชื่อ")]
        [MinLength(1)]
        [MaxLength(100)]
        [RegularExpression(@"^[a-zA-Zก-๙\s]+$", ErrorMessage = "ชื่อลูกค้าต้องเป็นตัวอักษรเท่านั้น ห้ามใช้สัญลักษณ์พิเศษหรือตัวเลข")]
        public string Name { get; private set; }

        [Required(ErrorMessage = "กรุณากรอกอีเมล")]
        [MinLength(10)]
        [MaxLength(500)]
        [EmailAddress]
        public string Email { get; private set; }

        [Required(ErrorMessage = "กรุณากรอกรหัสผ่าน")]
        [MinLength(1)]
        [MaxLength(100)]
        public string Password { get; private set; }
        public UserRoles Role { get; private set; } = UserRoles.User;

        public DateTime CreatedDate { get; private set; } = DateTime.UtcNow;
        public string? CreatedBy { get; private set; } = null;

        // JWT & Refresh Token fields
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiresAt { get; set; }
        public DateTime? LastLoginAt { get; set; }

        protected User() { } // 👈 2. เปลี่ยนชื่อ Constructor ให้ตรงกับคลาส

        public User(string name, string email, string password, UserRoles role = UserRoles.User) // 👈 3. เปลี่ยนชื่อ Constructor ด้วย
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("กรุณาระบุชื่อ", nameof(name));
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("กรุณากรอกอีเมล", nameof(email));
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("กรุณากรอกรหัสผ่าน", nameof(password));
            Name = name;
            Email = email;
            Password = password;
            Role = role;
            CreatedDate = DateTime.UtcNow;
            CreatedBy = "System";
        }
    }
}