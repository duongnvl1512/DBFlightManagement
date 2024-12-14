using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DBFlightManagement.Data;
using DBFlightManagement.Models;

namespace DBFlightManagement.Pages.Tickets
{
    public class IndexModel : PageModel
    {
        private readonly DBFlightManagement.Data.ApplicationDbContext _context;

        public IndexModel(DBFlightManagement.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Ticket> Ticket { get;set; } = default!;
        
        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; }
        public async Task OnGetAsync()
        {
            var query = _context.Ticket.AsQueryable();

            if (!string.IsNullOrEmpty(SearchTerm))
            {
                query = query.Where(c => c.DepartmentCity.Contains(SearchTerm) ||
                                         c.ArrivalCity.Contains(SearchTerm));
            }

            Ticket = await query.ToListAsync();
        }
    }
}
