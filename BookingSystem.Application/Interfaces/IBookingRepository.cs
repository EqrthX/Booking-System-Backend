using BookingSystem.Domain.Entities;


namespace BookingSystem.Application.Interfaces
{
    public interface IBookingRepository
    {
        Task<IEnumerable<Booking>> GetAllBookingsAsync();
        Task AddAsync(Booking booking);
        Task SaveChangesAsync();
    }
}
