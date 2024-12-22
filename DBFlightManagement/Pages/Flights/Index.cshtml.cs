using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DBFlightManagement.Data;
using DBFlightManagement.Models;
using Microsoft.AspNetCore.Authorization;

namespace DBFlightManagement.Pages.Flights
{
    [AllowAnonymous]
    public class IndexModel : PageModel
    {
        private readonly DBFlightManagement.Data.ApplicationDbContext _context;

        public IndexModel(DBFlightManagement.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Flight> Flight { get;set; } = default!;

        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; }

        public async Task OnGetAsync()
        {
            var query = _context.Flights.AsQueryable();
            if (!string.IsNullOrEmpty(SearchTerm))
            {
                query = query.Where(c => c.Airline.Contains(SearchTerm) ||
                                         c.DepartureAirport.Contains(SearchTerm) ||
                                         c.ArrivalAirport.Contains(SearchTerm) ||
                                         c.DepartureAirport.Contains(SearchTerm));
            }

            Flight = await query.ToListAsync();
        }
    }
}
