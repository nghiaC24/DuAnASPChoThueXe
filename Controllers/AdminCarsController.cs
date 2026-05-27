using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DuAnASPChoThueXe.Data;
using DuAnASPChoThueXe.Models;
using System.IO;

namespace DuAnASPChoThueXe.Controllers
{
    public class AdminCarsController : Controller
    {
        private readonly ApplicationDbContext _db;
        public AdminCarsController(ApplicationDbContext db) { _db = db; }

        public async Task<IActionResult> Index()
        {
            return View(await _db.Cars.ToListAsync());
        }

        public IActionResult Create() => View();

        // --- CHỨC NĂNG THÊM MỚI (Có kèm tải ảnh) ---
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Car car, IFormFile? ImageFile)
        {
            if (ModelState.IsValid)
            {
                // Xử lý upload ảnh nếu có chọn file
                if (ImageFile != null && ImageFile.Length > 0)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(ImageFile.FileName);
                    string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);

                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await ImageFile.CopyToAsync(stream);
                    }
                    car.ImageUrl = "/images/" + fileName;
                }

                _db.Cars.Add(car);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(car);
        }

        // --- CHỨC NĂNG SỬA XE (GET) ---
        public async Task<IActionResult> Edit(int id)
        {
            var car = await _db.Cars.FindAsync(id);
            if (car == null) return NotFound();
            return View(car);
        }

        // --- CHỨC NĂNG SỬA XE (POST) - CHỈ CẬP NHẬT TRẠNG THÁI ---
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Car car)
        {
            // Tìm xe gốc trong database
            var carInDb = await _db.Cars.FindAsync(id);
            if (carInDb == null) return NotFound();

            try
            {
                // CHỈ CẬP NHẬT TRẠNG THÁI
                carInDb.IsAvailable = car.IsAvailable;

                _db.Update(carInDb);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                return View(carInDb);
            }
        }

        // --- CHỨC NĂNG XÓA XE ---
        public async Task<IActionResult> Delete(int id)
        {
            var car = await _db.Cars.FindAsync(id);
            if (car != null)
            {
                _db.Cars.Remove(car);
                await _db.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}