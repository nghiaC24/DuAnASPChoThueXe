using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DuAnASPChoThueXe.Data;
using DuAnASPChoThueXe.Models;
using System.Linq;

namespace DuAnASPChoThueXe.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _db;

        public HomeController(ApplicationDbContext db)
        {
            _db = db;
        }

        // Trang chủ: Hiển thị danh sách xe còn trống
        public async Task<IActionResult> Index(string searchString, int? categoryId)
        {
            // LỌC: Chỉ lấy những xe có trạng thái "Sẵn sàng"
            var motorbikes = _db.Motorbikes
                .Include(m => m.Category)
                .Where(m => m.Status == "Sẵn sàng")
                .AsQueryable();

            // 1. Lọc theo loại xe
            if (categoryId.HasValue)
            {
                motorbikes = motorbikes.Where(m => m.CategoryId == categoryId);
            }

            // 2. Lọc theo từ khóa tìm kiếm
            if (!string.IsNullOrEmpty(searchString))
            {
                motorbikes = motorbikes.Where(s => s.Name.Contains(searchString));
            }

            return View(await motorbikes.ToListAsync());
        }

        // Trang chi tiết xe máy
        public async Task<IActionResult> Details(int id)
        {
            var motorbike = await _db.Motorbikes
                .Include(m => m.Category)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (motorbike == null) return NotFound();

            // Lấy 5 xe khác CÒN TRỐNG để hiện mục "Gợi ý thêm"
            ViewBag.OtherBikes = await _db.Motorbikes
                .Where(m => m.Id != id && m.Status == "Sẵn sàng")
                .Take(5)
                .ToListAsync();

            return View(motorbike);
        }

        // Trang riêng cho Xe Tay Ga
        public async Task<IActionResult> Scooters()
        {
            var list = await _db.Motorbikes
                .Include(m => m.Category)
                .Where(m => m.CategoryId == 1 && m.Status == "Sẵn sàng") // Lọc ID=1 và Sẵn sàng
                .ToListAsync();

            ViewBag.CategoryName = "Dòng Xe Tay Ga Cao Cấp";
            ViewBag.BannerImg = "https://img.freepik.com/free-photo/beautiful-girl-standing-near-her-scooter_23-2148905252.jpg";
            return View("CategoryPage", list);
        }

        // Trang riêng cho Xe Số
        public async Task<IActionResult> Manuals()
        {
            var list = await _db.Motorbikes
                .Include(m => m.Category)
                .Where(m => m.CategoryId == 2 && m.Status == "Sẵn sàng") // Lọc ID=2 và Sẵn sàng
                .ToListAsync();

            ViewBag.CategoryName = "Dòng Xe Máy Số Phổ Thông";
            ViewBag.BannerImg = "https://img.freepik.com/free-photo/young-lady-standing-near-scooter_23-2148905215.jpg";
            return View("CategoryPage", list);
        }

        // Trang danh sách Ô tô
        public async Task<IActionResult> Cars(string type)
        {
            // Chỉ lấy ô tô đang Sẵn sàng cho thuê (IsAvailable == true)
            var carsQuery = _db.Cars.Where(c => c.IsAvailable).AsQueryable();

            if (!string.IsNullOrEmpty(type))
            {
                if (type == "4" || type == "7")
                {
                    int seats = int.Parse(type);
                    carsQuery = carsQuery.Where(c => c.Seats == seats);
                }
            }

            return View(await carsQuery.ToListAsync());
        }

        // Chi tiết ô tô
        public async Task<IActionResult> CarDetails(int id)
        {
            var car = await _db.Cars.FirstOrDefaultAsync(m => m.Id == id);
            if (car == null || !car.IsAvailable) return NotFound();
            return View(car);
        }

        // Chi tiết xe máy (Dùng cho các link khác nếu có)
        public async Task<IActionResult> MotorbikeDetails(int id)
        {
            var bike = await _db.Motorbikes.Include(m => m.Category).FirstOrDefaultAsync(m => m.Id == id);
            if (bike == null || bike.Status != "Sẵn sàng") return NotFound();
            return View(bike);
        }

        public IActionResult About() => View();
        public IActionResult Contact() => View();
        public IActionResult Privacy() => View();

        [HttpPost]
        public IActionResult SendContact(string name, string phone, string message)
        {
            TempData["SuccessMessage"] = "Cảm ơn bạn! KNP Rental sẽ liên hệ lại với bạn sớm nhất.";
            return RedirectToAction("Contact");
        }
    }
}