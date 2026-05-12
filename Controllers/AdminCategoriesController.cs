using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DuAnASPChoThueXe.Data;
using DuAnASPChoThueXe.Models;

namespace DuAnASPChoThueXe.Controllers
{
    public class AdminCategoriesController : Controller
    {
        private readonly ApplicationDbContext _db;
        public AdminCategoriesController(ApplicationDbContext db) { _db = db; }

        public async Task<IActionResult> Index() { return View(await _db.Categories.ToListAsync()); }

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(Category category)
        {
            _db.Categories.Add(category);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var cat = await _db.Categories.FindAsync(id);
            if (cat != null) { _db.Categories.Remove(cat); await _db.SaveChangesAsync(); }
            return RedirectToAction(nameof(Index));
        }
    }
}