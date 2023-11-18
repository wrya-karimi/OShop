using Microsoft.EntityFrameworkCore;
using OShop.Domain.Abstracts.Repositories.CategoryRepositories;
using OShop.Domain.Entities;
using OShop.Infrastructures.Persistence.Contexts;

namespace OShop.Infrastructures.Persistence.Respositories.CategoryRepositories
{
    public class CategoryReadRepository : ICategoryReadRepository
    {
        private readonly ApplicationDbContext _context;

        public CategoryReadRepository(ApplicationDbContext context) => _context = context;

        public async Task<Category?> FindByIdAsync(int id)
        {
            return await _context.Set<Category>().FindAsync(id);
        }

        public async Task<IEnumerable<Category>> GetAllAsync(params string[] IncludeProperties)
        {
            var query = Include(IncludeProperties);
            if (query != null)
            {
                return await query.ToListAsync();
            }
            return await _context.Set<Category>().ToListAsync();
        }

        private IQueryable<Category>? Include(params string[] IncludeProperties)
        {
            if (IncludeProperties != null && IncludeProperties.Length > 0)
            {
                IQueryable<Category>? query = null;
                foreach (var prop in IncludeProperties)
                {
                    query = _context.Set<Category>().Include(prop);
                }
                return query;
            }
            return null;
        }
    }
}
