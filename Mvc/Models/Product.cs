using System.ComponentModel.DataAnnotations;

namespace Mvc.Models
{
    public class Product
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Please enter the product name")]
        public required string Name { get; set; }
        [Required]
        public decimal Price { get; set; }
        public string? Photo { get; set; }
    }
}
