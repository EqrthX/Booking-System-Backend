using BookingSystem.Application.Interfaces;
using BookingSystem.Domain.Entities;
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
            var existingBooking = await _context.Bookings.FirstOrDefaultAsync(b => b.CustomerName == booking.CustomerName && b.TimeSlot == booking.TimeSlot);
            if (existingBooking == null)
            {
                await _context.Bookings.AddAsync(booking);
            } 
            else
            {
                throw new InvalidOperationException("มีลูกค้าจองเวลานี้ไปเรียบร้อยแล้ว กรุณาเลือกเวลาใหม่");
            }
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
