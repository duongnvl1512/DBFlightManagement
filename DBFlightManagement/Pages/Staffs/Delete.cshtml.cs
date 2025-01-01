using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DBFlightManagement.Data;
using DBFlightManagement.Models;

namespace DBFlightManagement.Pages.Staffs
{
    public class DeleteModel : PageModel
    {
        private readonly DBFlightManagement.Data.ApplicationDbContext _context;

        public DeleteModel(DBFlightManagement.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Staff Staff { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var staff = await _context.Staffs.FirstOrDefaultAsync(m => m.StaffId == id);

            if (staff == null)
            {
                return NotFound();
            }
            else
            {
                Staff = staff;
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
            var staff = await _context.Staffs
                                         .Include(c => c.IdentityUser)
                                         .FirstOrDefaultAsync(c => c.StaffId == id);
            if (staff != null)
            {
                // Xóa đối tượng Customer
                _context.Staffs.Remove(staff);

                // Nếu có IdentityUser liên quan, xóa cả IdentityUser
                if (staff.IdentityUser != null)
                {
                    _context.Users.Remove(staff.IdentityUser);
                }

                // Lưu các thay đổi vào cơ sở dữ liệu
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
