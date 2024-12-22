using DBFlightManagement.Data;
using DBFlightManagement.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DBFlightManagement.Pages.Tickets
{
    [Authorize]
    public class BuyTicketModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public BuyTicketModel(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [BindProperty]
        public BuyTicketViewModel TicketViewModel { get; set; } = new BuyTicketViewModel();

        public IActionResult OnGet(int flightId)
        {
            // Tìm chuyến bay
            var flight = _context.Flights.FirstOrDefault(f => f.FlightId == flightId);

            if (flight == null)
            {
                // Không tìm thấy chuyến bay
                TicketViewModel.IsFlightFound = false;
                TicketViewModel.ErrorMessage = $"Flight with ID {flightId} not found.";
            }
            else
            {
                // Tìm thấy chuyến bay
                TicketViewModel.IsFlightFound = true;
                TicketViewModel.Flight = flight;
            }

            return Page();
        }

        public IActionResult OnPost(int flightId, int seatCount, string paymentMethod)
        {
            // Kiểm tra người dùng đăng nhập
            var userId = _userManager.GetUserId(User);
            if (userId == null)
            {
                TicketViewModel.ErrorMessage = "User is not logged in.";
                return Page();
            }

            // Tìm khách hàng
            var customer = _context.Customers.FirstOrDefault(c => c.AspNetUserId == userId);
            if (customer == null)
            {
                TicketViewModel.ErrorMessage = "Customer profile not found.";
                return Page();
            }

            // Tìm chuyến bay
            var flight = _context.Flights.FirstOrDefault(f => f.FlightId == flightId);
            if (flight == null)
            {
                TicketViewModel.ErrorMessage = "Flight not found.";
                return Page();
            }

            // Kiểm tra số ghế
            if (seatCount <= 0 || seatCount > flight.AvailableSeats)
            {
                TicketViewModel.ErrorMessage = "Invalid seat count.";
                return Page();
            }

            // Tính tổng tiền
            var totalPrice = flight.TicketPrice * seatCount;

            // Tạo vé (ticket)
            var ticket = new Ticket
            {
                FlightId = flight.FlightId,
                CustomerId = customer.CustomerId,
                BookingDate = DateTime.Now,
                SeatBooked = seatCount,
                TotalPrice = totalPrice,
                PaymentStatus = true,
                PaymentMethod = paymentMethod
            };
            _context.Tickets.Add(ticket);

            // Cập nhật số ghế còn lại
            flight.AvailableSeats -= seatCount;
            _context.Flights.Update(flight);

            // Lưu DB
            _context.SaveChanges();

            // Hiển thị thông báo thành công
            TicketViewModel.IsFlightFound = true;
            TicketViewModel.Flight = flight;
            TicketViewModel.ErrorMessage = $"Payment successful! Your ticket ID is {ticket.TicketId}.";

            return Page();
        }
    }
}
