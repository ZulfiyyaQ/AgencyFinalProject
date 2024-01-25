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
    public class SettingController : Controller
    {
        private readonly AppDbContext _context;

        public SettingController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            List<Setting> setting = await _context.Settings.ToListAsync();
            return View(setting);
        }
        public async Task<IActionResult> Update(int id)
        {

            if (id <= 0) return BadRequest();
            Setting existed = await _context.Settings.FirstOrDefaultAsync(p => p.Id == id);
            if (existed is null) return NotFound();
            UpdateSettingVM vm= new UpdateSettingVM
            {
                Key= existed.Key,
                Value=existed.Value,
            };
            return View(vm);
        }
        [HttpPost]
        public async Task<IActionResult> Update(int id, UpdateSettingVM vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            if (id <= 0) return BadRequest();
            Setting existed = await _context.Settings.FirstOrDefaultAsync(p => p.Id == id);
            if (existed is null) return NotFound();
            var result = _context.Settings.Any(p => p.Key.ToLower().Trim() == vm.Key.ToLower().Trim() && p.Id != id);
            if (result)
            {

                ModelState.AddModelError("Name", "Bele setting artiq movcutdur");
                return View(vm);
            }
            existed.Key = vm.Key;
            existed.Value = vm.Value;

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
