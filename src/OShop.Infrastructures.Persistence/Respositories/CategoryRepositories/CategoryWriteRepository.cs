using OShop.Domain.Abstracts.Repositories.CategoryRepositories;
using OShop.Domain.Entities;
using OShop.Infrastructures.Persistence.Contexts;

namespace OShop.Infrastructures.Persistence.Respositories.CategoryRepositories
{
    public class CategoryWriteRepository : ICategoryWriteRepository
    {
        private readonly ApplicationDbContext _context;

        public CategoryWriteRepository(ApplicationDbContext context) => _context = context;

        public async Task<Category?> FindByIdAsync(int id)
        {
            return await _context.Set<Category>().FindAsync(id);
        }

        public async Task AddAsync(Category category)
        {
            if (category == null)
                throw new ArgumentNullException(nameof(category));
            category.CreateAt = DateTime.UtcNow;
            await _context.Set<Category>().AddAsync(category);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveAsync(Category category)
        {
            if (category == null)
                throw new ArgumentNullException(nameof(category));

            _context.Set<Category>().Remove(category);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Category category)
        {
            if (category == null)
                throw new ArgumentNullException(nameof(category));

            category.LastModifiedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();
        }
    }
}
