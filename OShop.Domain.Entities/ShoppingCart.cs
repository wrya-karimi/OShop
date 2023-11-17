using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OShop.Domain.Entities
{
    public class ShoppingCart : BaseEntity
    {
        public virtual ICollection<Itemcart>? Products { get; set; } // relationship one-to-many
    }
}
