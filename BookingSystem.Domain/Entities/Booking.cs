using BookingSystem.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookingSystem.Domain.Entities
{
    [Table("Bookings")]
    public class Booking
    {
        [Key]
        public Guid Id { get; private set; }

        [ForeignKey(nameof(User))]
        public int UserId { get; private set; }

        [ForeignKey(nameof(Resource))]
        public int ResourceId { get; private set; }

        public DateTime BookingDate { get; private set; }
        public DateTime CheckInTime { get; private set; }
        public DateTime CheckOutTime { get; private set; }

        public BookingStatus Status { get; private set; } = BookingStatus.Confirmed;

        [MaxLength(500)]
        public string? Notes { get; set; }

        public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
        public DateTime? ConfirmedAt { get; set; }
        public DateTime? CancelledAt { get; set; }

        // Navigation Properties
        public User? User { get; set; }
        public Resource? Resource { get; set; }

        protected Booking() { }

        public Booking(int userId, int resourceId, DateTime checkInTime, DateTime checkOutTime)
        {
            if (userId <= 0)
                throw new ArgumentException("User ID must be valid.", nameof(userId));

            if (resourceId <= 0)
                throw new ArgumentException("Resource ID must be valid.", nameof(resourceId));

            if (checkInTime >= checkOutTime)
                throw new ArgumentException("Check-in time must be before check-out time.");

            if (checkInTime < DateTime.UtcNow)
                throw new ArgumentException("Check-in time must be in the future.");

            Id = Guid.NewGuid();
            UserId = userId;
            ResourceId = resourceId;
            BookingDate = DateTime.UtcNow;
            CheckInTime = checkInTime;
            CheckOutTime = checkOutTime;
            Status = BookingStatus.Confirmed;
            ConfirmedAt = DateTime.UtcNow;
        }

        public void Confirm()
        {
            if (Status == BookingStatus.Cancelled)
                throw new InvalidOperationException("Cannot confirm a cancelled booking.");

            Status = BookingStatus.Confirmed;
            ConfirmedAt = DateTime.UtcNow;
        }

        public void Cancel()
        {
            if (Status == BookingStatus.Confirmed)
                throw new InvalidOperationException("Cannot cancel a confirmed booking.");

            Status = BookingStatus.Cancelled;
            CancelledAt = DateTime.UtcNow;
        }
    }
}
