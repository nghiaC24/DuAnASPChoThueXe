using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DuAnASPChoThueXe.Data;
using System.Threading.Tasks;
using System.Linq;

namespace DuAnASPChoThueXe.Controllers
{
    public class AdminBookingsController : Controller
    {
        private readonly ApplicationDbContext _db;
        public AdminBookingsController(ApplicationDbContext db) { _db = db; }

        // 1. Hiển thị danh sách đơn hàng
        public async Task<IActionResult> Index()
        {
            // Include cả 2 loại xe để hiển thị tên xe trên danh sách đơn
            var bookings = await _db.Bookings
                .Include(b => b.Motorbike)
                .Include(b => b.Car)
                .OrderByDescending(b => b.Id)
                .ToListAsync();
            return View(bookings);
        }

        // 2. Cập nhật trạng thái và tự động quản lý số lượng xe
        [HttpPost]
        public async Task<IActionResult> UpdateStatus(int id, string status)
        {
            var booking = await _db.Bookings
                .Include(b => b.Motorbike)
                .Include(b => b.Car)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (booking == null) return NotFound();

            string oldStatus = booking.Status;
            booking.Status = status;

            // Logic tự động trừ/cộng kho khi thay đổi trạng thái
            // Giao xe -> Trừ kho
            if (status == "Đã giao xe" && oldStatus != "Đã giao xe")
            {
                if (booking.Motorbike != null && booking.Motorbike.AvailableQuantity > 0)
                    booking.Motorbike.AvailableQuantity--;

                if (booking.Car != null && booking.Car.AvailableQuantity > 0)
                    booking.Car.AvailableQuantity--;
            }
            // Hoàn thành/Hủy sau khi đã giao -> Cộng lại kho
            else if ((status == "Hoàn thành" || status == "Đã hủy") && oldStatus == "Đã giao xe")
            {
                if (booking.Motorbike != null)
                    booking.Motorbike.AvailableQuantity++;

                if (booking.Car != null)
                    booking.Car.AvailableQuantity++;
            }

            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        // 3. Chi tiết đơn hàng và GPS
        public async Task<IActionResult> Details(int id)
        {
            // QUAN TRỌNG: Bạn phải thêm .Include(b => b.Car) ở đây
            // Trước đó code của bạn chỉ Include Motorbike nên khi thuê Ô tô dữ liệu sẽ bị trống
            var booking = await _db.Bookings
                .Include(b => b.Motorbike)
                .Include(b => b.Car)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (booking == null) return NotFound();

            return View(booking);
        }
    }
}