using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Respositories.ProductRepository
{
    public interface IProductRepository
    {
        Task<Product?> FindByIdAsync(int id);
        Task AddAsync(Product product);
        Task UpdateAsync(Product product);
        Task<IEnumerable<Product>> GetAllAsync(params string[] IncludeProperties);
        Task RemoveAsync(Product product);
    }
}
