using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using OShop.Domain.Abstracts.Repositories.BaseRepositories;
using OShop.Domain.Abstracts.Repositories.CategoryRepositories;
using OShop.Domain.Entities;
using OShop.Infrastructures.Persistence.Contexts;

namespace OShop.Infrastructures.Persistence.Respositories.CategoryRepositories
{
    public class CacheCategoryRepository : ICategoryRepository
    {
        private readonly CategoryRepository _decorated;
        private readonly IDistributedCache _distributedCache;
        private readonly ApplicationDbContext _context;

        public CacheCategoryRepository(CategoryRepository decorated, IDistributedCache distributedCache, ApplicationDbContext context)
        {
            _decorated = decorated;
            _distributedCache = distributedCache;
            _context = context;
        }

        public async Task<Category?> FindByIdAsync(int id)
        {
            string key = $"category{id}";

            string? cachedCategory = await _distributedCache.GetStringAsync(key);
            Category? category;
            if (string.IsNullOrEmpty(cachedCategory))
            {
                category = await _decorated.FindByIdAsync(id);
                if (category is null)
                {
                    return category;
                }

                await _distributedCache.SetStringAsync(key, JsonConvert.SerializeObject(category));
                return category;
            }

            category = JsonConvert.DeserializeObject<Category>(cachedCategory);

            if (category is not null)
            {
                _context.Set<Category>().Attach(category);
            }

            return category;
        }

        public Task<IEnumerable<Category>> GetAllAsync(params string[] IncludeProperties)
        {
            return _decorated.GetAllAsync(IncludeProperties);
        }

        public Task AddAsync(Category entity)
        {
            return _decorated.AddAsync(entity);
        }

        public Task UpdateAsync(Category entity)
        {
            return _decorated.UpdateAsync(entity);
        }

        public Task DeleteAsync(int id)
        {
            return _decorated.DeleteAsync(id);
        }
    }
}
