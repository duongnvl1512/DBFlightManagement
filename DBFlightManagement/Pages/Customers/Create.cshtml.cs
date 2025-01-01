using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using DBFlightManagement.Data;
using DBFlightManagement.Models;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace DBFlightManagement.Pages.Customers
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public CreateModel(ApplicationDbContext context,
                           UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required, EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required, DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [Required, DataType(DataType.Password)]
            [Compare(nameof(Password), ErrorMessage = "Passwords do not match.")]
            [Display(Name = "Confirm Password")]
            public string ConfirmPassword { get; set; }

            [Required, StringLength(50)]
            [Display(Name = "First Name")]
            public string FirstName { get; set; }

            [Required, StringLength(50)]
            [Display(Name = "Last Name")]
            public string LastName { get; set; }

            [Required(ErrorMessage = "Personal ID is required.")]
            [StringLength(20)]
            public string PersonalId { get; set; }

            [StringLength(20)]
            [Display(Name = "Phone Number")]
            public string PhoneNumber { get; set; }

            [StringLength(200)]
            [Display(Name = "Address")]
            public string Address { get; set; }
        }

        public void OnGet()
        {
            // Hiển thị form
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // 1. Tạo IdentityUser
            var user = new IdentityUser
            {
                UserName = Input.Email,
                Email = Input.Email,
                PhoneNumber = Input.PhoneNumber,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, Input.Password);

            if (!result.Succeeded)
            {
                // Hiển thị lỗi nếu Identity không thành công (mật khẩu yếu, email trùng...)
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return Page();
            }

            // Gán role "Customer" cho user
            var roleResult = await _userManager.AddToRoleAsync(user, "Customer");
            if (!roleResult.Succeeded)
            {
                // Hiển thị lỗi nếu việc gán role thất bại
                foreach (var error in roleResult.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return Page();
            }

            // 2. Tạo Customer và gán Email
            var newCustomer = new Customer
            {
                AspNetUserId = user.Id,    // Liên kết IdentityUser
                Email = user.Email,        // Gán Email từ IdentityUser
                FirstName = Input.FirstName,
                LastName = Input.LastName,
                PersonalId = Input.PersonalId,
                PhoneNumber = Input.PhoneNumber,
                Address = Input.Address
            };

            _context.Customers.Add(newCustomer);
            await _context.SaveChangesAsync();

            // 3. Chuyển hướng về trang danh sách hoặc chi tiết
            return RedirectToPage("Index");
        }

    }
}
