using FinalProjectAgency.Areas.Admin.ViewModels;
using FinalProjectAgency.DAL;
using FinalProjectAgency.Models;
using FinalProjectAgency.Utilities.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinalProjectAgency.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize (Roles ="admin")]
    public class ProductController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public ProductController(AppDbContext context,IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public async Task<IActionResult> Index()
        {
            List<Product> product = await  _context.Products.Include(p=>p.Categories).ToListAsync();
            return View(product);
        }
        public async Task<IActionResult> Create()
        {
            CreateProductVM vm = new CreateProductVM();
            GetList(vm);
            return View(vm);

        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateProductVM vm)
        {
            if(!ModelState.IsValid)
            {
                GetList(vm);
                return View(vm);
            }
            var result  = _context.Products.Any(p=>p.Name.ToLower().Trim()==vm.Name.ToLower().Trim());
            if(result)
            {
                GetList(vm);
                ModelState.AddModelError("Name", "Bele product artiq movcutdur");
                return View(vm);
            }
            var result2 = _context.Categories.Any(p => p.Id==vm.CategoryId);
            if (!result2)
            {
                GetList(vm);
                ModelState.AddModelError("Id", "Bele Category Tapilmadi");
                return View(vm);
            }
            if(!vm.Photo.ValidateType())
            {
                GetList(vm);
                ModelState.AddModelError("Photo", "Tipi uygun deyil");
                return View(vm);
            }
            if (!vm.Photo.ValidateSize())
            {
                GetList(vm);
                ModelState.AddModelError("Photo", "Olcusu uygun deyil");
                return View(vm);
            }
            string filename = await vm.Photo.CreateFile(_env.WebRootPath, "img", "portfolio");
            Product product = new Product 
            {
                Name= vm.Name,
                CategoryId=(int)vm.CategoryId,
                Image=filename,
            };
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }
        public async Task<IActionResult> Update(int id)
        {
            if (id <= 0) return BadRequest();
            Product existed = await _context.Products.Include(p => p.Categories).FirstOrDefaultAsync(p=>p.Id==id);
            if(existed is null) return NotFound();
            UpdateProductVM vm = new UpdateProductVM
            {
                Name = existed.Name,
                Image = existed.Image,
                CategoryId = (int)existed.CategoryId,
                Categories = await _context.Categories.ToListAsync()
            };
            return View(vm);
        }
        [HttpPost]
        public async Task<IActionResult> Update(int id,UpdateProductVM vm)
        {
            if (!ModelState.IsValid)
            {
                GetList(vm);
                return View(vm);
            }
            Product existed = await _context.Products.Include(p => p.Categories).FirstOrDefaultAsync(p => p.Id == id);
            if (existed is null) return NotFound();
            var result = _context.Products.Any(p => p.Name.ToLower().Trim() == vm.Name.ToLower().Trim()&&p.Id!=id);
            if (result)
            {
                GetList(vm);
                ModelState.AddModelError("Name", "Bele product artiq movcutdur");
                return View(vm);
            }
            if(vm.Photo is not null)
            {
                if (!vm.Photo.ValidateType())
                {
                    GetList(vm);
                    ModelState.AddModelError("Photo", "Tipi uygun deyil");
                    return View(vm);
                }
                if (!vm.Photo.ValidateSize())
                {
                    GetList(vm);
                    ModelState.AddModelError("Photo", "Olcusu uygun deyil");
                    return View(vm);
                }
                string newimage=  await vm.Photo.CreateFile(_env.WebRootPath, "img", "portfolio");
                existed.Image.DeleteFile(_env.WebRootPath, "img", "portfolio");
                existed.Image = newimage;
            }
            existed.Name= vm.Name;
            existed.CategoryId= vm.CategoryId;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0) return BadRequest();
            Product existed = await _context.Products.Include(p => p.Categories).FirstOrDefaultAsync(p => p.Id == id);
            if (existed is null) return NotFound();
            _context.Products.Remove(existed);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Detail(int id)
        {
            if (id <= 0) return BadRequest();
            Product existed = await _context.Products.Include(p => p.Categories).FirstOrDefaultAsync(p => p.Id == id);
            if (existed is null) return NotFound();
            return View(existed);


        }
        private void GetList(CreateProductVM vm)
        {
            vm.Categories= _context.Categories.ToList();
        }
        private void GetList(UpdateProductVM vm)
        {
            vm.Categories = _context.Categories.ToList();
        }
    }
}
