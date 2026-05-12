using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DuAnASPChoThueXe.Data;
using DuAnASPChoThueXe.Models;

namespace DuAnASPChoThueXe.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _db;

        public HomeController(ApplicationDbContext db)
        {
            _db = db;
        }

        // Trang chủ: Hiển thị danh sách xe, Tìm kiếm và Lọc theo loại xe
        public async Task<IActionResult> Index(string searchString, int? categoryId)
        {
            // Lấy danh sách xe và nạp thông tin danh mục
            var motorbikes = _db.Motorbikes.Include(m => m.Category).AsQueryable();

            // 1. Lọc theo loại xe (Xe tay ga ID=1, Xe số ID=2)
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

        // Trang chi tiết xe
        public async Task<IActionResult> Details(int id)
        {
            var motorbike = await _db.Motorbikes
                .Include(m => m.Category)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (motorbike == null) return NotFound();

            // Lấy 5 xe khác để hiện mục "Thuê nhiều nhất"
            ViewBag.OtherBikes = await _db.Motorbikes
                .Where(m => m.Id != id)
                .Take(5)
                .ToListAsync();

            return View(motorbike);
        }

        // Trang giới thiệu
        public IActionResult About()
        {
            return View();
        }
        public IActionResult Contact()
        {
            return View();
        }
        // --- Thêm đoạn này vào HomeController.cs ---

        // Trang riêng cho Xe Tay Ga
        public async Task<IActionResult> Scooters()
        {
            var list = await _db.Motorbikes
                .Include(m => m.Category)
                .Where(m => m.CategoryId == 1) // Giả sử ID=1 là Xe Ga
                .ToListAsync();

            ViewBag.CategoryName = "Dòng Xe Tay Ga Cao Cấp";
            ViewBag.BannerImg = "https://img.freepik.com/free-photo/beautiful-girl-standing-near-her-scooter_23-2148905252.jpg";
            return View("CategoryPage", list); // Dùng chung 1 file giao diện cho gọn
        }

        // Trang riêng cho Xe Số
        public async Task<IActionResult> Manuals()
        {
            var list = await _db.Motorbikes
                .Include(m => m.Category)
                .Where(m => m.CategoryId == 2) // Giả sử ID=2 là Xe Số
                .ToListAsync();

            ViewBag.CategoryName = "Dòng Xe Máy Số Phổ Thông";
            ViewBag.BannerImg = "https://img.freepik.com/free-photo/young-lady-standing-near-scooter_23-2148905215.jpg";
            return View("CategoryPage", list);
        }

        [HttpPost]
        public IActionResult SendContact(string name, string phone, string message)
        {
            // Ở đây sau này bạn có thể viết code gửi mail hoặc lưu vào database
            TempData["Success"] = "Cảm ơn bạn! KNP Rental sẽ liên hệ lại với bạn sớm nhất.";
            return RedirectToAction("Contact");
        }

        public IActionResult Privacy() => View();
    }
}