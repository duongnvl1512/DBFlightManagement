using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DBFlightManagement.Data;
using DBFlightManagement.Models;

namespace DBFlightManagement.Pages.Flights
{
    public class DeleteModel : PageModel
    {
        private readonly DBFlightManagement.Data.ApplicationDbContext _context;

        public DeleteModel(DBFlightManagement.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Flight Flight { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var flight = await _context.Flight.FirstOrDefaultAsync(m => m.FlightId == id);

            if (flight == null)
            {
                return NotFound();
            }
            else
            {
                Flight = flight;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var flight = await _context.Flight.FindAsync(id);
            if (flight != null)
            {
                Flight = flight;
                _context.Flight.Remove(Flight);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
