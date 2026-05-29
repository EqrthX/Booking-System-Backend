
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BookingSystem.Domain.Enums;

namespace BookingSystem.Domain.Entities
{
    [Table("Resources")]
    public class Resource
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string RoomName { get; set; } = string.Empty;

        public TypeRoom typeRoom { get; set; }

        public int Capacity { get; set; }

        [MaxLength(500)]
        public string Description { get; set; } = string.Empty;

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        // Navigation Properties
        public ResourceDetail? ResourceDetail { get; set; }
        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
    }
}