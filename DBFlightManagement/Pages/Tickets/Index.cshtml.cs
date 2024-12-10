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

        public async Task OnGetAsync()
        {
            Ticket = await _context.Ticket.ToListAsync();
        }
    }
}
