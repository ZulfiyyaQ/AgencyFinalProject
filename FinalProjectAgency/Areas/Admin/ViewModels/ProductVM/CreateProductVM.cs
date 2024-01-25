using FinalProjectAgency.Models;
using Microsoft.AspNetCore.Http;

namespace FinalProjectAgency.Areas.Admin.ViewModels
{
    public class CreateProductVM
    {
        public string Name { get; set; }
        
        public List<Category>? Categories { get; set; }
        public int CategoryId { get; set; }
        public IFormFile Photo { get; set; }
    }
}
