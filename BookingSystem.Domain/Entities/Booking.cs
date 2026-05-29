using BookingSystem.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSystem.Domain.Entities
{
    public class Booking
    {
        public Guid Id { get; private set; }
        public string CustomerName { get; private set; } = string.Empty;
        public DateTime TimeSlot { get; private set; }
        public BookingStatus Status { get; private set; }

        public Booking(string customerName, DateTime timeSlot)
        {
            if(string.IsNullOrWhiteSpace(customerName))
            {
                throw new ArgumentException("Customer name is required.", nameof(customerName));
            }

            if(timeSlot < DateTime.UtcNow)
            {
                throw new ArgumentException("Time slot must be in the future.", nameof(timeSlot));
            }

            Id = Guid.NewGuid();
            CustomerName = customerName;
            TimeSlot = timeSlot;
            Status = BookingStatus.Pending;
        }

        public void Confirm()
        {
            if (Status == BookingStatus.Cancelled)
                throw new InvalidOperationException("Cannot confirm a cancelled booking.");

            Status = BookingStatus.Confirmed;
        }

        public void Cancel()
        {
            if (Status == BookingStatus.Confirmed)
                throw new InvalidOperationException("Cannot cancel a confirmed booking.");

            Status = BookingStatus.Cancelled;
        }
    }
}
