using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using DBFlightManagement.Data;
using DBFlightManagement.Models;

namespace DBFlightManagement.Pages.Flights
{
    public class CreateModel : PageModel
    {
        private readonly DBFlightManagement.Data.ApplicationDbContext _context;

        public CreateModel(DBFlightManagement.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Flight Flight { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Flight.Add(Flight);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
