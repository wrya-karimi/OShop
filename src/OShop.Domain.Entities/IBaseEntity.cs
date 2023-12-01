using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OShop.Domain.Entities
{
    public interface IBaseEntity
    {
        DateTime CreateAt { get; set; }
        DateTime LastModifiedAt { get; set; }
    }
}
