using System.ComponentModel.DataAnnotations;

namespace DBFlightManagement.Models
{
    public class Flight
    {
        public int FlightId { get; set; }
        public string Airline { get; set; }
        public string DepartureAirport { get; set; }
        public string ArrivalAirport { get; set; }
        [DataType(DataType.Date)]
        public DateTime DepartureDateTime { get; set; }
        public int AvailableSeats { get; set; }
        public float TicketPrice { get; set; }
    }
}
