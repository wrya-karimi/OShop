using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Data.Respositories.ProductRepository
{
    public class ProductRepository : IProductRepository

    {
        private readonly ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context) => _context = context;

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

        public async Task<IEnumerable<Product>> GetAllAsync(params string[] IncludeProperties)
        {
            var query = Include(IncludeProperties);
            if (query != null)
            {
                return await query.ToListAsync();
            }
            return await _context.Set<Product>().ToListAsync();
        }


        private IQueryable<Product>? Include(params string[] IncludeProperties)
        {
            if (IncludeProperties != null && IncludeProperties.Length > 0)
            {
                IQueryable<Product>? query = null;
                foreach (var prop in IncludeProperties)
                {
                    query = _context.Set<Product>().Include(prop);
                }
                return query;
            }
            return null;
        }
    }
}
