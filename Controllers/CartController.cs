using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DuAnASPChoThueXe.Data;
using DuAnASPChoThueXe.Models;
using Microsoft.AspNetCore.Http;

namespace DuAnASPChoThueXe.Controllers
{
    public class CartController : Controller
    {
        private readonly ApplicationDbContext _db;
        public CartController(ApplicationDbContext db) { _db = db; }

        public IActionResult Index()
        {
            var cartItems = _db.CartItems
                               .Include(c => c.Motorbike)
                               .Include(c => c.Car)
                               .ToList();
            return View(cartItems);
        }

        [HttpPost]
        [IgnoreAntiforgeryToken]
        public IActionResult AddToCart(int? motorbikeId, int? carId)
        {
            var userEmail = HttpContext.Session.GetString("UserEmail");
            if (string.IsNullOrEmpty(userEmail))
            {
                return Json(new { success = false, isNotLoggedIn = true });
            }

            // 1. KIỂM TRA: Xe này đã có trong giỏ hàng chưa?
            var existingItem = _db.CartItems.FirstOrDefault(c =>
                (motorbikeId != null && c.MotorbikeId == motorbikeId) ||
                (carId != null && c.CarId == carId));

            if (existingItem != null)
            {
                // NẾU ĐÃ CÓ: Không cho cộng dồn x2, x3. Trả về thông báo lỗi.
                return Json(new { success = false, message = "Xe này đã có trong giỏ hàng của bạn rồi!" });
            }

            // 2. NẾU CHƯA CÓ: Tiến hành thêm mới vào giỏ với số lượng mặc định là 1
            decimal price = 0;
            string plate = "";

            if (motorbikeId != null)
            {
                var xe = _db.Motorbikes.Find(motorbikeId);
                if (xe != null)
                {
                    price = xe.PricePerDay;
                    plate = xe.LicensePlate ?? "---";
                }
            }
            else if (carId != null)
            {
                var oto = _db.Cars.Find(carId);
                if (oto != null)
                {
                    price = oto.PricePerDay;
                    plate = oto.LicensePlate ?? "---";
                }
            }

            var cartItem = new CartItem
            {
                MotorbikeId = motorbikeId,
                CarId = carId,
                Quantity = 1, // Luôn luôn là 1
                Price = price,
                LicensePlate = plate
            };

            _db.CartItems.Add(cartItem);
            _db.SaveChanges();

            return Json(new { success = true });
        }

        public IActionResult GetCartPartial()
        {
            var items = _db.CartItems
                .Include(c => c.Motorbike)
                .Include(c => c.Car)
                .ToList();
            return PartialView("_CartModalPartial", items);
        }

        [HttpPost]
        public IActionResult ProcessCheckout(string customerName, string customerPhone, string citizenId,
                                     string address, string district, string ward, string note)
        {
            // 1. Kiểm tra đăng nhập
            var userEmail = HttpContext.Session.GetString("UserEmail");
            if (string.IsNullOrEmpty(userEmail)) return RedirectToAction("Login", "Account");

            var cartItems = _db.CartItems.ToList();
            if (!cartItems.Any()) return RedirectToAction("Index", "Home");

            // 2. Lặp qua giỏ hàng và lưu vào bảng Booking
            foreach (var item in cartItems)
            {
                var booking = new Booking
                {
                    CustomerName = customerName,
                    CustomerPhone = customerPhone,
                    CitizenId = citizenId, // LƯU CCCD VÀO ĐÂY
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now.AddDays(1),
                    TotalPrice = item.Price * item.Quantity,
                    Status = "Chờ xác nhận",
                    MotorbikeId = item.MotorbikeId,
                    CarId = item.CarId
                };
                _db.Bookings.Add(booking);

                // Khóa xe (Logic bạn đã làm ở bước trước)
                if (item.MotorbikeId.HasValue)
                {
                    var bike = _db.Motorbikes.Find(item.MotorbikeId);
                    if (bike != null) bike.Status = "Đang thuê";
                }
            }

            // 3. Xóa giỏ hàng và lưu thay đổi
            _db.CartItems.RemoveRange(cartItems);
            _db.SaveChanges();

            TempData["SuccessMessage"] = "Đặt xe thành công!";
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