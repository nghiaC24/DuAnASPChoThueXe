using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DuAnASPChoThueXe.Data;

namespace DuAnASPChoThueXe.Controllers
{
    public class AdminBookingsController : Controller
    {
        private readonly ApplicationDbContext _db;
        public AdminBookingsController(ApplicationDbContext db) { _db = db; }

        public async Task<IActionResult> Index()
        {
            // Lấy danh sách đơn hàng và thông tin xe được thuê
            var bookings = await _db.Bookings.Include(b => b.Motorbike).OrderByDescending(b => b.Id).ToListAsync();
            return View(bookings);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateStatus(int id, string status)
        {
            var booking = await _db.Bookings.FindAsync(id);
            if (booking != null) { booking.Status = status; await _db.SaveChangesAsync(); }
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Details(int id)
        {
            // Lấy thông tin đơn hàng và xe máy tương ứng
            var booking = await _db.Bookings
                .Include(b => b.Motorbike)
                .ThenInclude(m => m.Category)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (booking == null) return NotFound();

            return View(booking);
        }
    }
}