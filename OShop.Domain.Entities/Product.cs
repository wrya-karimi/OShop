using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OShop.Domain.Entities
{
    public class Product : BaseEntity
    {
        public required string Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public string? ImagePath { get; set; }
        public int View { get; set; }
        public required int CategoryId { get; set; }
        public virtual Category? Category { get; set; }
    }
}
