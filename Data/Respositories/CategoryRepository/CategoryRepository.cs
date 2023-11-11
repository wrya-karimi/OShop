using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Data.Respositories.CategoryRepository
{
    public class CategoryRepository : ICategoryRepository

    {
        private readonly ApplicationDbContext _context;

        public CategoryRepository(ApplicationDbContext context) => _context = context;

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
