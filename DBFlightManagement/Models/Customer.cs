using System.ComponentModel.DataAnnotations;

namespace DBFlightManagement.Models
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }
        public int Gender { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
    }
}
