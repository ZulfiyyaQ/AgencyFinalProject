using FinalProjectAgency.DAL;
using FinalProjectAgency.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace FinalProjectAgency.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context )
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            List<Product> product= await _context.Products.Include(p=>p.Categories).ToListAsync();
            return View(product);
        }
    }
}