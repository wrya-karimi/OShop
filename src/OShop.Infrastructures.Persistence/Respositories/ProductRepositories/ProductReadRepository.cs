using Microsoft.EntityFrameworkCore;
using OShop.Domain.Abstracts.Repositories.ProductRepositories;
using OShop.Domain.Entities;
using OShop.Infrastructures.Persistence.Contexts;

namespace OShop.Infrastructures.Persistence.Respositories.ProductRepositories
{
    public class ProductReadRepository : IProductReadRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductReadRepository(ApplicationDbContext context) => _context = context;

        public async Task<Product?> FindByIdAsync(int id)
        {
            return await _context.Set<Product>().FindAsync(id);
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
