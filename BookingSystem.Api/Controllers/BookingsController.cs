using BookingSystem.Api.Hubs;
using BookingSystem.Application.Services;
using BookingSystem.Domain.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace BookingSystem.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookingsController : ControllerBase
    {
        private readonly BookingService _bookingService;
        private readonly IHubContext<BookingHub> _hubContext;
        public BookingsController(BookingService bookingService, IHubContext<BookingHub> hubContext)
        {
            _bookingService = bookingService;
            _hubContext = hubContext;
        }

        [HttpPost]
        public async Task<IActionResult> CreateBooking([FromBody] CreateBookingRequest request)
        {
            if (request == null)
            {
                return BadRequest("กรุณากรอกข้อมูลให้ครบ");
            }

            try
            {
                var booking = await _bookingService.CreateBookingAsync(request.UserId, request.ResourceId, request.CheckInTime, request.CheckOutTime);

                await _hubContext.Clients.All.SendAsync("BookingAdded", booking);
                return Ok(booking);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
