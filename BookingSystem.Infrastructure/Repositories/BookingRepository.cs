using BookingSystem.Application.Interfaces;
using BookingSystem.Domain.Entities;
using BookingSystem.Domain.Enums;
using BookingSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BookingSystem.Infrastructure.Repositories
{
    public class BookingRepository : IBookingRepository
    {
        private readonly ApplicationDbContext _context;
        public BookingRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Booking>> GetAllBookingsAsync()
        {
            return await _context.Bookings.ToListAsync();
        }

        public async Task AddAsync(Booking booking)
        {
            // Check if resource is already booked during this time slot
            var existingBooking = await _context.Bookings
                .Where(b => b.ResourceId == booking.ResourceId
                    && b.Status == BookingStatus.Confirmed
                    && b.CheckInTime < booking.CheckOutTime
                    && b.CheckOutTime > booking.CheckInTime)
                .FirstOrDefaultAsync();

            if (existingBooking != null)
            {
                throw new InvalidOperationException("ห้องนี้ถูกจองไปแล้วในเวลานี้ กรุณาเลือกเวลาใหม่");
            }

            await _context.Bookings.AddAsync(booking);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
