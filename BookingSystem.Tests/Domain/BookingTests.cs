using BookingSystem.Domain.Entities;
using BookingSystem.Domain.Enums;


namespace BookingSystem.Tests.Domain
{
    public class BookingTests
    {
        [Fact]
        public void CreateBooking_WithValidData_ShouldSetStatusToPending()
        {
            // Arrange (เตรียมข้อมูล)
            var customerName = "John Doe";
            var futureDate = DateTime.UtcNow.AddDays(1);

            // Act (ลงมือทำ)
            var booking = new Booking(customerName, futureDate);

            // Assert (ตรวจสอบผลลัพธ์)
            Assert.Equal(customerName, booking.CustomerName);
            Assert.Equal(BookingStatus.Pending, booking.Status);
        }

        [Fact]
        public void ConfirmBooking_WhenPending_ShouldChangeStatusToConfirmed()
        {
            var booking = new Booking("Jane Doe", DateTime.UtcNow.AddDays(1));

            // Act
            booking.Confirm();

            // Assert
            Assert.Equal(BookingStatus.Confirmed, booking.Status);
        }

        [Fact]
        public void CreateBooking_WithPastDate_ShouldThrowException()
        {
            // Arrange
            var pastDate = DateTime.UtcNow.AddDays(-1);

            // Act & Assert
            Assert.Throws<ArgumentException>(() => new Booking("Bob", pastDate));
        }
    }

}