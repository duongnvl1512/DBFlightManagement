using System.ComponentModel.DataAnnotations;

namespace DBFlightManagement.Models
{
    public class Staff
    {
        public int StaffId { get; set; }
        public string Name { get; set; }
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }
        public int Gender { get; set; }
        public string Position { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
    }
}
