using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookingSystem.Domain.Entities
{
    [Table("ResourceDetails")]
    public class ResourceDetail
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey(nameof(Resource))]
        public int ResourceId { get; set; }

        // Room Details
        [MaxLength(1000)]
        public string? Features { get; set; }

        [MaxLength(1000)]
        public string? Amenities { get; set; }

        [MaxLength(2000)]
        public string? RoomImages { get; set; } // JSON array of image URLs

        [MaxLength(500)]
        public string? Location { get; set; }

        public decimal PricePerHour { get; set; }
        public decimal? PricePerDay { get; set; }

        [MaxLength(100)]
        public string? ContactPerson { get; set; }

        [MaxLength(20)]
        public string? PhoneNumber { get; set; }

        public bool IsActive { get; set; } = true;
        public DateTime LastUpdated { get; set; } = DateTime.UtcNow;

        // Navigation Property
        public Resource? Resource { get; set; }

        protected ResourceDetail() { }

        public ResourceDetail(int resourceId)
        {
            ResourceId = resourceId;
        }
    }
}