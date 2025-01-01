using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DBFlightManagement.Data;
using DBFlightManagement.Models;

namespace DBFlightManagement.Pages.Customers
{
    public class IndexModel : PageModel
    {
        private readonly DBFlightManagement.Data.ApplicationDbContext _context;

        public IndexModel(DBFlightManagement.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Customer> Customer { get;set; } = default!;

        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; }

        public async Task OnGetAsync()
        {
            var query = _context.Customers.AsQueryable();
            if (!string.IsNullOrEmpty(SearchTerm))
            {
                query = query.Where(c => c.Email.Contains(SearchTerm) ||
                                         c.FirstName.Contains(SearchTerm) ||
                                         c.LastName.Contains(SearchTerm) ||
                                         c.PersonalId.Contains(SearchTerm) ||
                                         c.PhoneNumber.Contains(SearchTerm) ||
                                         c.Address.Contains(SearchTerm));
            }

            Customer = await query.ToListAsync();
        }
    }
}
