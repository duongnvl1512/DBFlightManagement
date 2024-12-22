namespace DBFlightManagement.Models
{
    public class BuyTicketViewModel
    {
        public bool IsFlightFound { get; set; } // Để kiểm tra chuyến bay có tồn tại hay không
        public Flight Flight { get; set; }      // Thông tin chuyến bay
        public string ErrorMessage { get; set; } // Thông báo lỗi (nếu có)
    }
}
