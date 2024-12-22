using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DBFlightManagement.Models
{
    public class Ticket
    {
        [Key]
        public int TicketId { get; set; }

        // Khóa ngoại trỏ đến Flight
        [Required]
        public int FlightId { get; set; }

        // Khóa ngoại trỏ đến Customer
        [Required]
        public int CustomerId { get; set; }

        // Ngày đặt vé
        [Required]
        public DateTime BookingDate { get; set; }

        // Số ghế đặt
        [Range(1, int.MaxValue, ErrorMessage = "At least 1 seat must be booked.")]
        public int SeatBooked { get; set; }

        // Giá trị tổng tiền (Flight.TicketPrice * SeatBooked, hoặc +Sale, -Discount, ...)
        [Range(0, double.MaxValue, ErrorMessage = "TotalPrice must be >= 0.")]
        public decimal TotalPrice { get; set; }

        [Required]
        public bool PaymentStatus { get; set; } // false = unpaid, true = paid

        [StringLength(50)]
        public string PaymentMethod { get; set; } // E.g., CreditCard, PayPal

        // Navigation Properties
        [ForeignKey(nameof(FlightId))]
        public virtual Flight? Flight { get; set; }

        [ForeignKey(nameof(CustomerId))]
        public virtual Customer? Customer { get; set; }
    }
}
