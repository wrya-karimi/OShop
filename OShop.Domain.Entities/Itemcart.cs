﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OShop.Domain.Entities
{
    public class Itemcart : BaseEntity
    {
        public int Quantity { get; set; }

        public int ProductId { get; set; }

        public virtual Product? Product { get; set; }
    }
}
