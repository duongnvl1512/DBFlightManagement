using DBFlightManagement.Constants;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DBFlightManagement.Models
{
    public class Customer
    {
        [Key]
        public int CustomerId { get; set; }

        // AspNetUserId là string (GUID) từ IdentityUser
        [Required]
        public string AspNetUserId { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        [StringLength(100)]
        public string Email { get; set; }

        [Required(ErrorMessage = "First Name is required.")]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required.")]
        [StringLength(50)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Personal ID is required.")]
        [StringLength(20)]
        public string PersonalId { get; set; }

        [Phone(ErrorMessage = "Invalid phone number.")]
        [StringLength(20)]
        public string? PhoneNumber { get; set; }

        [StringLength(200)]
        public string? Address { get; set; }

        // Navigation Property: Một customer có thể có nhiều ticket
        public virtual ICollection<Ticket>? Tickets { get; set; }

        // Navigation Property: Tham chiếu sang bảng AspNetUsers (Identity)
        // Không bắt buộc phải Include, tùy theo nhu cầu
        [ForeignKey(nameof(AspNetUserId))]
        public virtual IdentityUser? IdentityUser { get; set; }
    }
}
