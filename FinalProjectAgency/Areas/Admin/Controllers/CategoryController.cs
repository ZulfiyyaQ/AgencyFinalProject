using FinalProjectAgency.Areas.Admin.ViewModels;
using FinalProjectAgency.Areas.Admin.ViewModels;
using FinalProjectAgency.DAL;
using FinalProjectAgency.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace FinalProjectAgency.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "admin")]
    public class CategoryController : Controller
    {
        private readonly AppDbContext _context;

        public CategoryController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            List<Category> category = await _context.Categories.Include(c => c.Products).ToListAsync();
            return View(category);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateCategoryVM vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            var result = _context.Categories.Any(p => p.Name.ToLower().Trim() == vm.Name.ToLower().Trim());
            if (result)
            {
              
                ModelState.AddModelError("Name", "Bele category artiq movcutdur");
                return View(vm);
            }
            Category category = new Category
            {
                Name = vm.Name,
               
            };
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task< IActionResult> Update(int id)
        {

            if (id <= 0) return BadRequest();
            Category existed = await _context.Categories.Include(p => p.Products).FirstOrDefaultAsync(p => p.Id == id);
            if (existed is null) return NotFound();
            UpdateProductVM vm = new UpdateProductVM
            {
                Name = existed.Name,
                
            };
            return View(vm);
        }
        [HttpPost]
        public async Task<IActionResult> Update(int id,UpdateCategoryVM vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            if (id <= 0) return BadRequest();
            Category existed = await _context.Categories.Include(p => p.Products).FirstOrDefaultAsync(p => p.Id == id);
            if (existed is null) return NotFound();
            var result = _context.Categories.Any(p => p.Name.ToLower().Trim() == vm.Name.ToLower().Trim()&&p.Id!=id);
            if (result)
            {

                ModelState.AddModelError("Name", "Bele category artiq movcutdur");
                return View(vm);
            }
            existed.Name = vm.Name;
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0) return BadRequest();
            Category existed = await _context.Categories.Include(p => p.Products).FirstOrDefaultAsync(p => p.Id == id);
            if (existed is null) return NotFound();
            _context.Categories.Remove(existed);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Detail(int id)
        {
            if (id <= 0) return BadRequest();
            Category existed = await _context.Categories.Include(p => p.Products).FirstOrDefaultAsync(p => p.Id == id);
            if (existed is null) return NotFound();

            return View(existed);
        }
    }
}
