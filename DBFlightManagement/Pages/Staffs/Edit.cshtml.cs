using DBFlightManagement.Data;
using DBFlightManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DBFlightManagement.Pages.Staffs
{
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public EditModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Staff Input { get; set; }

        public IActionResult OnGet(int id)
        {
            var staff = _context.Staffs.Find(id);
            if (staff == null) return NotFound();

            Input = staff;
            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid) return Page();

            var existing = _context.Staffs.Find(Input.StaffId);
            if (existing == null) return NotFound();

            existing.FirstName = Input.FirstName;
            existing.LastName = Input.LastName;
            existing.PersonalId = Input.PersonalId;
            existing.PhoneNumber = Input.PhoneNumber;
            existing.Address = Input.Address;
            existing.Position = Input.Position;

            _context.Staffs.Update(existing);
            _context.SaveChanges();

            return RedirectToPage("Index");
        }
    }
}
