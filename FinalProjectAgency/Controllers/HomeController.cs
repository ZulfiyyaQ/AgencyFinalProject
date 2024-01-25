using FinalProjectAgency.DAL;
using FinalProjectAgency.Models;
using Microsoft.AspNetCore.Mvc;
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
            return View();
        }
    }
}