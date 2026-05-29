using BookingSystem.Application.Interfaces;
using BookingSystem.Domain.Entities;

namespace BookingSystem.Application.Services
{
    public class BookingService
    {
        private readonly IBookingRepository _bookingRepository;

        public BookingService(IBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }

        public async Task<Booking> CreateBookingAsync(int userId, int resourceId, DateTime checkInTime, DateTime checkOutTime)
        {
            var booking = new Booking(userId, resourceId, checkInTime, checkOutTime);
            await _bookingRepository.AddAsync(booking);
            await _bookingRepository.SaveChangesAsync();
            return booking;
        }
    }
}
