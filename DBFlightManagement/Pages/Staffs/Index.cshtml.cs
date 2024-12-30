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

namespace DBFlightManagement.Pages.Staffs
{
    [Authorize(Roles = "Admin,Staff")]
    public class IndexModel : PageModel
    {
        private readonly DBFlightManagement.Data.ApplicationDbContext _context;

        public IndexModel(DBFlightManagement.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Staff> Staff { get; set; } = default!;

        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; }

        public async Task OnGetAsync()
        {
            var query = _context.Staffs.AsQueryable();
            if (!string.IsNullOrEmpty(SearchTerm))
            {
                query = query.Where(s => s.Email.Contains(SearchTerm) ||
                                         s.FirstName.Contains(SearchTerm) ||
                                         s.LastName.Contains(SearchTerm) ||
                                         s.PersonalId.Contains(SearchTerm) ||
                                         s.PhoneNumber.Contains(SearchTerm) ||
                                         s.Address.Contains(SearchTerm));
            }

            Staff = await query.ToListAsync();
        }
    }
}
