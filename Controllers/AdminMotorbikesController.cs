using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DuAnASPChoThueXe.Data;
using DuAnASPChoThueXe.Models;

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

        // 1. Danh sách xe
        public async Task<IActionResult> Index()
        {
            var list = await _db.Motorbikes.Include(m => m.Category).ToListAsync();
            return View(list);
        }

        // 2. Thêm mới xe
        public IActionResult Create()
        {
            ViewBag.Categories = new SelectList(_db.Categories, "Id", "Name");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Motorbike bike, IFormFile? file)
        {
            if (file != null)
            {
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                string path = Path.Combine(_env.WebRootPath, "images", fileName);
                using (var stream = new FileStream(path, FileMode.Create)) { file.CopyTo(stream); }
                bike.ImageUrl = "/images/" + fileName;
            }
            _db.Motorbikes.Add(bike);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        // 3. Sửa xe
        public async Task<IActionResult> Edit(int id)
        {
            var bike = await _db.Motorbikes.FindAsync(id);
            ViewBag.Categories = new SelectList(_db.Categories, "Id", "Name", bike.CategoryId);
            return View(bike);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Motorbike bike, IFormFile? file)
        {
            if (file != null) // Nếu chọn ảnh mới
            {
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                string path = Path.Combine(_env.WebRootPath, "images", fileName);
                using (var stream = new FileStream(path, FileMode.Create)) { file.CopyTo(stream); }
                bike.ImageUrl = "/images/" + fileName;
            }
            _db.Update(bike);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        // 4. Xóa xe
        public async Task<IActionResult> Delete(int id)
        {
            var bike = await _db.Motorbikes.FindAsync(id);
            if (bike != null) { _db.Motorbikes.Remove(bike); await _db.SaveChangesAsync(); }
            return RedirectToAction("Index");
        }
    }
}