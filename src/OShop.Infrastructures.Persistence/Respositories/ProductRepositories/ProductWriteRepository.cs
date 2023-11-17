using OShop.Domain.Abstracts.Repositories.ProductRepositories;
using OShop.Domain.Entities;
using OShop.Infrastructures.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OShop.Infrastructures.Persistence.Respositories.ProductRepositories
{
    public class ProductWriteRepository: IProductWriteRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductWriteRepository(ApplicationDbContext context) => _context = context;

        public async Task<Product?> FindByIdAsync(int id)
        {
            return await _context.Set<Product>().FindAsync(id);
        }

        public async Task AddAsync(Product product)
        {
            if (product == null)
                throw new ArgumentNullException(nameof(product));
            product.CreateAt = DateTime.UtcNow;
            await _context.Set<Product>().AddAsync(product);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveAsync(Product product)
        {
            if (product == null)
                throw new ArgumentNullException(nameof(product));

            _context.Set<Product>().Remove(product);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Product product)
        {
            if (product == null)
                throw new ArgumentNullException(nameof(product));

            product.LastModifiedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();
        }
    }
}
