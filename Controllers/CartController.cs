using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DuAnASPChoThueXe.Data;
using DuAnASPChoThueXe.Models;

namespace DuAnASPChoThueXe.Controllers
{
    public class CartController : Controller
    {
        private readonly ApplicationDbContext _db;
        public CartController(ApplicationDbContext db) { _db = db; }

        public IActionResult Index()
        {
            var cartItems = _db.CartItems.Include(c => c.Motorbike).ToList();
            return View(cartItems);
        }

        [HttpPost]
        public IActionResult AddToCart(int motorbikeId)
        {
            var bike = _db.Motorbikes.Find(motorbikeId);
            if (bike == null) return Json(new { success = false });

            var item = _db.CartItems.FirstOrDefault(c => c.MotorbikeId == motorbikeId);
            if (item != null) { item.Quantity++; }
            else
            {
                _db.CartItems.Add(new CartItem { MotorbikeId = motorbikeId, Price = bike.PricePerDay, Quantity = 1 });
            }
            _db.SaveChanges();
            return Json(new { success = true });
        }

        public IActionResult GetCartPartial()
        {
            var cartItems = _db.CartItems.Include(c => c.Motorbike).ToList();
            return PartialView("_CartModalPartial", cartItems);
        }

        [HttpPost]
        public IActionResult ProcessCheckout(string customerName, string customerPhone, string address, string paymentMethod, string district, string ward)
        {
            // 1. Kiểm tra đăng nhập
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("UserEmail")))
            {
                return RedirectToAction("Login", "Account", new { returnUrl = "/Cart/Index" });
            }

            var items = _db.CartItems.ToList();
            if (!items.Any()) return RedirectToAction("Index", "Home");

            // 2. Lưu đơn hàng
            string fullAddress = $"{address}, {ward}, {district}, TP. Hồ Chí Minh";
            foreach (var item in items)
            {
                var booking = new Booking
                {
                    MotorbikeId = item.MotorbikeId,
                    CustomerName = customerName,
                    CustomerPhone = customerPhone,
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now.AddDays(1),
                    TotalPrice = item.Price * item.Quantity,
                    Status = (paymentMethod == "Transfer") ? "Đã chuyển khoản - Chờ xác nhận" : "Chờ nhận xe thanh toán"
                };
                _db.Bookings.Add(booking);
            }

            // 3. Xóa giỏ hàng & Điều hướng
            _db.CartItems.RemoveRange(items);
            _db.SaveChanges();

            TempData["SuccessMessage"] = "Đặt xe thành công! KNP Rental sẽ sớm liên hệ qua SĐT: " + customerPhone;
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Remove(int id)
        {
            var item = _db.CartItems.Find(id);
            if (item != null) { _db.CartItems.Remove(item); _db.SaveChanges(); }
            return RedirectToAction("Index");
        }
    }
}