using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DBFlightManagement.Data;
using DBFlightManagement.Models;

namespace DBFlightManagement.Pages.Flights
{
    public class EditModel : PageModel
    {
        private readonly DBFlightManagement.Data.ApplicationDbContext _context;

        public EditModel(DBFlightManagement.Data.ApplicationDbContext context)
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

            var flight =  await _context.Flight.FirstOrDefaultAsync(m => m.FlightId == id);
            if (flight == null)
            {
                return NotFound();
            }
            Flight = flight;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Flight).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FlightExists(Flight.FlightId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool FlightExists(int id)
        {
            return _context.Flight.Any(e => e.FlightId == id);
        }
    }
}
