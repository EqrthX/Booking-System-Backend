using System;
using System.Threading.Tasks;
using BookingSystem.Application.Interfaces;
using BookingSystem.Application.Services;
using BookingSystem.Domain.Entities;
using Moq;
using Xunit;

namespace BookingSystem.Tests.Application
{
    public class BookingServiceTests
    {
        [Fact]
        public async Task CreateBookingAsync_ShouldSaveToRepository()
        {
            // Arrange: จำลอง (Mock) Repository
            var mockRepo = new Mock<IBookingRepository>();
            var service = new BookingService(mockRepo.Object);
            var customerName = "Alice";
            var timeSlot = DateTime.UtcNow.AddDays(2);

            // Act: ทดสอบเรียกใช้ Service
            var result = await service.CreateBookingAsync(customerName, timeSlot);

            // Assert: ตรวจสอบว่า Entity ถูกสร้างและคืนค่ากลับมาถูกต้อง
            Assert.NotNull(result);
            Assert.Equal(customerName, result.CustomerName);

            // ตรวจสอบว่าฟังก์ชัน AddAsync และ SaveChangesAsync ถูกเรียกใช้งานจริงๆ อย่างละ 1 ครั้ง
            mockRepo.Verify(r => r.AddAsync(It.IsAny<Booking>()), Times.Once);
            mockRepo.Verify(r => r.SaveChangesAsync(), Times.Once);
        }
    }
}