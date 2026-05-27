using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DuAnASPChoThueXe.Data;
using DuAnASPChoThueXe.Models;
using System.IO;

namespace DuAnASPChoThueXe.Controllers
{
    public class AdminMotorbikesController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _env;

        public AdminMotorbikesController(ApplicationDbContext db, IWebHostEnvironment env)
        {
            _db = db;
            _env = env;
        }

        // Trang danh sách xe máy
        public async Task<IActionResult> Index()
        {
            var list = await _db.Motorbikes.Include(m => m.Category).ToListAsync();
            return View(list);
        }

        // Trang thêm xe mới (GET)
        public IActionResult Create()
        {
            ViewBag.Categories = new SelectList(_db.Categories, "Id", "Name");
            return View();
        }

        // Xử lý thêm xe mới (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Motorbike bike, IFormFile? file)
        {
            if (file != null)
            {
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                string path = Path.Combine(_env.WebRootPath, "images", fileName);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    file.CopyTo(stream);
                }
                bike.ImageUrl = "/images/" + fileName;
            }
            _db.Motorbikes.Add(bike);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        // Trang chỉnh sửa trạng thái (GET)
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var motorbike = await _db.Motorbikes.FindAsync(id);
            if (motorbike == null) return NotFound();
            return View(motorbike);
        }

        // Xử lý lưu trạng thái Bật/Tắt (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, bool IsReady)
        {
            // Tìm xe gốc trong Database để bảo trì dữ liệu cũ
            var motorbikeInDb = await _db.Motorbikes.FindAsync(id);

            if (motorbikeInDb == null) return NotFound();

            try
            {
                // CẬP NHẬT TRẠNG THÁI: 
                // Nếu nút gạt (IsReady) là True -> Gán chữ "Sẵn sàng"
                // Nếu nút gạt (IsReady) là False -> Gán chữ "Bảo trì"
                motorbikeInDb.Status = IsReady ? "Sẵn sàng" : "Bảo trì";

                _db.Update(motorbikeInDb);
                await _db.SaveChangesAsync();

                // Lưu thành công thì quay về trang danh sách
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                // Nếu có lỗi, hiện lại trang sửa với dữ liệu hiện tại
                return View(motorbikeInDb);
            }
        }

        // Xử lý xóa xe
        public async Task<IActionResult> Delete(int id)
        {
            var bike = await _db.Motorbikes.FindAsync(id);
            if (bike != null)
            {
                _db.Motorbikes.Remove(bike);
                await _db.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }
    }
}