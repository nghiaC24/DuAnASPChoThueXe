using Microsoft.AspNetCore.Mvc;
using DuAnASPChoThueXe.Data;
using DuAnASPChoThueXe.Models;
using Microsoft.AspNetCore.Http;
using System.Linq;

namespace DuAnASPChoThueXe.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _db;
        public AccountController(ApplicationDbContext db) { _db = db; }

        // Trang Đăng nhập
        public IActionResult Login(string returnUrl = null)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        public IActionResult Login(string email, string password, string returnUrl = null)
        {
            // Tìm người dùng trong Database
            var user = _db.Users.FirstOrDefault(u => u.Email == email && u.Password == password);

            if (user != null)
            {
                // Lưu thông tin vào Session để dùng ở Layout
                HttpContext.Session.SetString("UserEmail", user.Email);
                HttpContext.Session.SetString("UserName", user.FullName);
                HttpContext.Session.SetString("UserRole", user.Role ?? "Customer");

                // Nếu là ADMIN: Bay thẳng vào trang quản trị xe
                if (user.Role == "Admin")
                {
                    return RedirectToAction("Index", "AdminMotorbikes");
                }

                // Nếu là khách: Quay lại trang đang xem hoặc về trang chủ
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

            // Mặc định đăng ký mới là Khách (Customer)
            user.Role = "Customer";

            _db.Users.Add(user);
            _db.SaveChanges();

            TempData["Success"] = "Đăng ký thành công! Mời bạn đăng nhập.";
            return RedirectToAction("Login");
        }

        // Đăng xuất
        public IActionResult Logout()
        {
            HttpContext.Session.Clear(); // Xóa sạch bộ nhớ đăng nhập
            return RedirectToAction("Index", "Home");
        }
    }
}