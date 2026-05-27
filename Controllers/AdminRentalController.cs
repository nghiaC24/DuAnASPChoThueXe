using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DuAnASPChoThueXe.Data;
using DuAnASPChoThueXe.Models;

namespace DuAnASPChoThueXe.Controllers
{
    public class AdminRentalController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminRentalController(ApplicationDbContext context)
        {
            _context = context;
        }

        // 1. TRANG DANH SÁCH KHÁCH THUÊ (Đã sửa để lấy đủ biển số Ô tô/Xe máy)
        public IActionResult Index(string searchString)
        {
            var dataQuery = _context.Bookings
                .Include(b => b.Motorbike)
                .Include(b => b.Car)
                .AsQueryable();

            // Lọc theo tìm kiếm nếu có
            if (!string.IsNullOrEmpty(searchString))
            {
                dataQuery = dataQuery.Where(b => b.CustomerName.Contains(searchString)
                                             || b.CustomerPhone.Contains(searchString)
                                             || (b.Motorbike != null && b.Motorbike.LicensePlate.Contains(searchString))
                                             || (b.Car != null && b.Car.LicensePlate.Contains(searchString)));
            }

            var data = dataQuery
                .OrderByDescending(b => b.StartDate)
                .Select(b => new RentalViewModel
                {
                    IdDonThue = b.Id,
                    TenKhachHang = b.CustomerName,
                    SoDienThoai = b.CustomerPhone,
                    TenXe = b.Motorbike != null ? b.Motorbike.Name : (b.Car != null ? b.Car.Name : "N/A"),
                    BienSoXe = b.Motorbike != null ? b.Motorbike.LicensePlate : (b.Car != null ? b.Car.LicensePlate : "---"),
                    NgayThue = b.StartDate,
                    NgayTraThucTe = b.ActualReturnDate
                }).ToList();

            ViewBag.CurrentSearch = searchString;
            return View(data);
        }

        // 2. LOGIC XÁC NHẬN TRẢ XE: CẬP NHẬT ĐƠN HÀNG VÀ MỞ KHÓA XE TRONG KHO
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmReturn(int id)
        {
            var booking = await _context.Bookings
                .Include(b => b.Motorbike)
                .Include(b => b.Car)
                .FirstOrDefaultAsync(b => b.Id == id);

            if (booking != null)
            {
                // Bước A: Cập nhật thông tin đơn thuê (Lưu lịch sử)
                booking.ActualReturnDate = DateTime.Now;
                booking.Status = "Đã hoàn thành - Đã trả xe";

                // Bước B: MỞ KHÓA XE MÁY (Để hiện lại trên website)
                if (booking.Motorbike != null)
                {
                    booking.Motorbike.Status = "Sẵn sàng";
                    booking.Motorbike.AvailableQuantity = 1;
                }

                // Bước C: MỞ KHÓA Ô TÔ (Để hiện lại trên website)
                if (booking.Car != null)
                {
                    booking.Car.IsAvailable = true;
                    booking.Car.AvailableQuantity = 1;
                }

                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Xác nhận trả xe thành công!";
            }

            return RedirectToAction(nameof(Index));
        }
    }
}