using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Category : BaseEntity
    {
        [MaxLength(50)]
        public required string CategoryName { get; set; }

        [MaxLength(100)]
        public string? Description { get; set; }

        public virtual ICollection<Product>? Products { get; set; }
    }
}
