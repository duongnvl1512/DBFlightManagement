using DBFlightManagement.Data;
using DBFlightManagement.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DBFlightManagement.Pages.Tickets
{
    [Authorize]
    public class DetailsModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DetailsModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public Ticket Ticket { get; set; }

        public IActionResult OnGet(int ticketId)
        {
            Ticket = _context.Tickets.FirstOrDefault(t => t.TicketId == ticketId);
            if (Ticket == null)
            {
                return NotFound();
            }

            return Page();
        }
    }
}
