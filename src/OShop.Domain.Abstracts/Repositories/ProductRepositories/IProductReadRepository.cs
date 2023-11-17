using OShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OShop.Domain.Abstracts.Repositories.ProductRepositories
{
    public interface IProductReadRepository
    {
        Task<Product?> FindByIdAsync(int id);
        Task<IEnumerable<Product>> GetAllAsync(params string[] IncludeProperties);
    }
}
