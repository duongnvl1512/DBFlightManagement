using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using DBFlightManagement.Models;

namespace DBFlightManagement.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<DBFlightManagement.Models.Customer> Customer { get; set; } = default!;
        public DbSet<DBFlightManagement.Models.Flight> Flight { get; set; } = default!;
        public DbSet<DBFlightManagement.Models.Staff> Staff { get; set; } = default!;
        public DbSet<DBFlightManagement.Models.Ticket> Ticket { get; set; } = default!;
    }
}
