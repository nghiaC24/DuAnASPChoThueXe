using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DuAnASPChoThueXe.Data;
using System.Linq;

namespace DuAnASPChoThueXe.Controllers
{
    public class AdminDashboardController : Controller
    {
        private readonly ApplicationDbContext _db;
        public AdminDashboardController(ApplicationDbContext db) { _db = db; }

        public async Task<IActionResult> Index()
        {
            // 1. Tổng doanh thu (Chỉ tính các đơn đã Hoàn thành)
            var completedBookings = _db.Bookings.Where(b => b.Status == "Hoàn thành");

            ViewBag.TotalRevenue = await completedBookings.SumAsync(b => b.TotalPrice);

            // 2. Doanh thu hôm nay
            var today = DateTime.Today;
            ViewBag.TodayRevenue = await completedBookings
                .Where(b => b.StartDate.Date == today)
                .SumAsync(b => b.TotalPrice);

            // 3. Doanh thu tháng này
            var currentMonth = DateTime.Now.Month;
            var currentYear = DateTime.Now.Year;
            ViewBag.MonthRevenue = await completedBookings
                .Where(b => b.StartDate.Month == currentMonth && b.StartDate.Year == currentYear)
                .SumAsync(b => b.TotalPrice);

            // 4. Thống kê theo ngày (Lấy 7 ngày gần nhất)
            var dailyStats = await completedBookings
                .GroupBy(b => b.StartDate.Date)
                .Select(g => new { Date = g.Key, Amount = g.Sum(b => b.TotalPrice) })
                .OrderByDescending(g => g.Date)
                .Take(7)
                .ToListAsync();
            ViewBag.DailyStats = dailyStats;

            // 5. Tổng số đơn hàng
            ViewBag.TotalOrders = await _db.Bookings.CountAsync();

            return View();
        }
    }
}