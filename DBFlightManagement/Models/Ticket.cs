using System.ComponentModel.DataAnnotations;

namespace DBFlightManagement.Models
{
    public class Ticket
    {
        public int TicketId { get; set; }
        public int FlightId { get; set; }
        public int CustomerId { get; set; }
        [DataType(DataType.Date)]
        public DateTime BookingDate { get; set; }
        public int SeatBooked { get; set; }
        public float? Sale { get; set; }
        public float TotalPrice { get; set; }
    }
}
