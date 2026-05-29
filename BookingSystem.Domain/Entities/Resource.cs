
using System.ComponentModel.DataAnnotations;
using BookingSystem.Domain.Enums;

namespace BookingSystem.Domain.Entities
{
    public class Resource
    {
        [Key]
        public int Id { get; set; }

        public string RoomName { get; set; } = string.Empty;

        public TypeRoom typeRoom { get; set; }

        public int Capacity { get; set; }
        public string Description { get; set; } = string.Empty;


    }
}