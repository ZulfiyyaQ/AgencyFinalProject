namespace FinalProjectAgency.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Image { get; set; }
        public Category Categories { get; set; }
        public int CategoryId { get; set; }
    }
}
