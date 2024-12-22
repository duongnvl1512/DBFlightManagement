using System.ComponentModel.DataAnnotations;

namespace DBFlightManagement.Models
{
    public class Flight
    {
        [Key]
        public int FlightId { get; set; }

        [Required(ErrorMessage = "Airline is required.")]
        [StringLength(100)]
        public string Airline { get; set; }

        [Required(ErrorMessage = "Departure Airport is required.")]
        [StringLength(50)]
        public string DepartureAirport { get; set; }

        [Required(ErrorMessage = "Arrival Airport is required.")]
        [StringLength(50)]
        public string ArrivalAirport { get; set; }

        // Ngày giờ khởi hành
        [Required(ErrorMessage = "Departure DateTime is required.")]
        public DateTime DepartureDateTime { get; set; }

        // Số ghế còn trống
        [Range(0, int.MaxValue, ErrorMessage = "Available seats must be >= 0.")]
        public int AvailableSeats { get; set; }

        // Giá vé cơ bản (1 ghế). Nên dùng decimal cho tiền tệ.
        [Range(0, double.MaxValue, ErrorMessage = "Ticket price must be >= 0.")]
        public decimal TicketPrice { get; set; }

        // Navigation Property: Một Flight có thể có nhiều Ticket
        public virtual ICollection<Ticket>? Tickets { get; set; }
    }
}
