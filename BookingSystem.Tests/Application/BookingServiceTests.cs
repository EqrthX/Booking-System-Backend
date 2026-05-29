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
            int userId = 1;
            int resourceId = 1;
            var checkInTime = DateTime.UtcNow.AddDays(2);
            var checkOutTime = DateTime.UtcNow.AddDays(2).AddHours(2);

            // Act: ทดสอบเรียกใช้ Service
            var result = await service.CreateBookingAsync(userId, resourceId, checkInTime, checkOutTime);

            // Assert: ตรวจสอบว่า Entity ถูกสร้างและคืนค่ากลับมาถูกต้อง
            Assert.NotNull(result);
            Assert.Equal(userId, result.UserId);
            Assert.Equal(resourceId, result.ResourceId);

            // ตรวจสอบว่าฟังก์ชัน AddAsync และ SaveChangesAsync ถูกเรียกใช้งานจริงๆ อย่างละ 1 ครั้ง
            mockRepo.Verify(r => r.AddAsync(It.IsAny<Booking>()), Times.Once);
            mockRepo.Verify(r => r.SaveChangesAsync(), Times.Once);
        }
    }
}