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
    public class DeleteModel : PageModel
    {
        private readonly DBFlightManagement.Data.ApplicationDbContext _context;

        public DeleteModel(DBFlightManagement.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Customer Customer { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers.FirstOrDefaultAsync(m => m.CustomerId == id);

            if (customer == null)
            {
                return NotFound();
            }
            else
            {
                Customer = customer;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Tìm đối tượng Customer dựa trên ID
            var customer = await _context.Customers
                                         .Include(c => c.IdentityUser)
                                         .FirstOrDefaultAsync(c => c.CustomerId == id);
            if (customer != null)
            {
                // Xóa đối tượng Customer
                _context.Customers.Remove(customer);

                // Nếu có IdentityUser liên quan, xóa cả IdentityUser
                if (customer.IdentityUser != null)
                {
                    _context.Users.Remove(customer.IdentityUser);
                }

                // Lưu các thay đổi vào cơ sở dữ liệu
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
