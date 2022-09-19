
using System.ComponentModel.DataAnnotations;

namespace Fullstack.API.Models
{
    public class MovieTicket
    {
        [Key]
        public Guid MovieId { get; set; }

        public string Name { get; set; }

        public long PhoneNumber { get; set; }

        public string MovieName { get; set; }

        public long Price { get; set; }

        public string SeatNumber { get; set; }

        public long AvailableTickets { get; set; }

    }
}
