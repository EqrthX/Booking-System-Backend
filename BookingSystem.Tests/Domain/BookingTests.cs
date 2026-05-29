using BookingSystem.Domain.Entities;
using BookingSystem.Domain.Enums;


namespace BookingSystem.Tests.Domain
{
    public class BookingTests
    {
        [Fact]
        public void CreateBooking_WithValidData_ShouldSetStatusToConfirmed()
        {
            // Arrange (เตรียมข้อมูล)
            int userId = 1;
            int resourceId = 1;
            var checkInTime = DateTime.UtcNow.AddDays(1);
            var checkOutTime = DateTime.UtcNow.AddDays(1).AddHours(2);

            // Act (ลงมือทำ)
            var booking = new Booking(userId, resourceId, checkInTime, checkOutTime);

            // Assert (ตรวจสอบผลลัพธ์)
            Assert.Equal(userId, booking.UserId);
            Assert.Equal(resourceId, booking.ResourceId);
            Assert.Equal(BookingStatus.Confirmed, booking.Status);
        }

        [Fact]
        public void CancelBooking_WhenConfirmed_ShouldChangeStatusToCancelled()
        {
            var booking = new Booking(1, 1, DateTime.UtcNow.AddDays(1), DateTime.UtcNow.AddDays(1).AddHours(2));

            // Act
            booking.Cancel();

            // Assert
            Assert.Equal(BookingStatus.Cancelled, booking.Status);
        }

        [Fact]
        public void CreateBooking_WithInvalidCheckInTime_ShouldThrowException()
        {
            // Arrange
            var pastDate = DateTime.UtcNow.AddDays(-1);
            var futureDate = DateTime.UtcNow.AddDays(1);

            // Act & Assert
            Assert.Throws<ArgumentException>(() => new Booking(1, 1, pastDate, futureDate));
        }
    }

}