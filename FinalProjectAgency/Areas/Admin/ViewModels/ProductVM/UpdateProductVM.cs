using FinalProjectAgency.Models;

namespace FinalProjectAgency.Areas.Admin.ViewModels
{
    public class UpdateProductVM
    {
        public string Name { get; set; }
        public string? Image { get; set; }
        public List<Category>? Categories { get; set; }
        public int CategoryId { get; set; }
        public IFormFile? Photo { get; set; }
    }
}
