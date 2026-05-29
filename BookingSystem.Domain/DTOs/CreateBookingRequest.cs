using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSystem.Domain.DTOs
{
    public class CreateBookingRequest
    {
        public string CustomerName { get; set; } = string.Empty;
        public DateTime TimeSlot { get; set; }
    }
}
