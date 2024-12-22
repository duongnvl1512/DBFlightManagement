using DBFlightManagement.Data;
using DBFlightManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DBFlightManagement.Pages.Customers
{
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public EditModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Customer Input { get; set; }

        public IActionResult OnGet(int id)
        {
            var customer = _context.Customers.Find(id);
            if (customer == null) return NotFound();

            Input = customer;
            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid) return Page();

            var existing = _context.Customers.Find(Input.CustomerId);
            if (existing == null) return NotFound();

            existing.FirstName = Input.FirstName;
            existing.LastName = Input.LastName;
            existing.PhoneNumber = Input.PhoneNumber;
            existing.Address = Input.Address;

            _context.Customers.Update(existing);
            _context.SaveChanges();

            return RedirectToPage("Index");
        }
    }
}
