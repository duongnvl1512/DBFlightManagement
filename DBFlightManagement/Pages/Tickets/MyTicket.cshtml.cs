using DBFlightManagement.Data;
using DBFlightManagement.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace DBFlightManagement.Pages.Tickets
{
    [Authorize]
    public class MyTicketsModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public MyTicketsModel(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public List<Ticket> Tickets { get; set; }

        public void OnGet()
        {
            var userId = _userManager.GetUserId(User);
            var customer = _context.Customers.FirstOrDefault(c => c.AspNetUserId == userId);

            if (customer != null)
            {
                Tickets = _context.Tickets
                    .Where(t => t.CustomerId == customer.CustomerId)
                    .ToList();
            }
            else
            {
                Tickets = new List<Ticket>();
            }
        }
    }
}
