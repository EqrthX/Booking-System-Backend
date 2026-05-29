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

        public async Task<Booking> CreateBookingAsync(string customerName, DateTime bookingDate)
        {
            var booking = new Booking(customerName, bookingDate);
            await _bookingRepository.AddAsync(booking);
            await _bookingRepository.SaveChangesAsync();
            return booking;
        }
    }
}
