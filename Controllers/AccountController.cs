using Microsoft.AspNetCore.Mvc;
using DuAnASPChoThueXe.Data;
using DuAnASPChoThueXe.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore; // Thêm thư viện này để dùng Include
using System.Linq;
using System.Threading.Tasks;

namespace DuAnASPChoThueXe.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _db;
        public AccountController(ApplicationDbContext db) { _db = db; }

        // ==========================================
        // 1. TRANG LỊCH SỬ THUÊ XE (MỚI THÊM)
        // ==========================================
        public async Task<IActionResult> RentalHistory()
        {
            // Kiểm tra xem khách đã đăng nhập chưa
            var userName = HttpContext.Session.GetString("UserName");
            if (string.IsNullOrEmpty(userName))
            {
                return RedirectToAction("Login");
            }

            // Lấy danh sách đơn thuê của người dùng này
            // Lọc theo CustomerName khớp với UserName trong Session
            var history = await _db.Bookings
                .Include(b => b.Motorbike)
                .Include(b => b.Car)
                .Where(b => b.CustomerName == userName)
                .OrderByDescending(b => b.StartDate) // Đơn mới nhất hiện lên đầu
                .ToListAsync();

            return View(history);
        }

        // Trang Đăng nhập
        public IActionResult Login(string returnUrl = null)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        public IActionResult Login(string email, string password, string returnUrl = null)
        {
            var user = _db.Users.FirstOrDefault(u => u.Email == email && u.Password == password);

            if (user != null)
            {
                HttpContext.Session.SetString("UserEmail", user.Email);
                HttpContext.Session.SetString("UserName", user.FullName);
                HttpContext.Session.SetString("UserRole", user.Role ?? "Customer");

                if (user.Role == "Admin")
                {
                    return RedirectToAction("Index", "AdminDashboard");
                }

                if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                    return Redirect(returnUrl);

                return RedirectToAction("Index", "Home");
            }

            ViewBag.Error = "Email hoặc mật khẩu không đúng!";
            return View();
        }

        // Trang Đăng ký
        public IActionResult Register() => View();

        [HttpPost]
        public IActionResult Register(User user)
        {
            if (_db.Users.Any(u => u.Email == user.Email))
            {
                ViewBag.Error = "Email này đã có người sử dụng!";
                return View(user);
            }

            user.Role = "Customer";
            _db.Users.Add(user);
            _db.SaveChanges();

            TempData["Success"] = "Đăng ký thành công! Mời bạn đăng nhập.";
            return RedirectToAction("Login");
        }

        // Đăng xuất
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}