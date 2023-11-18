using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using OShop.Domain.Abstracts.Repositories.CategoryRepositories;
using OShop.Domain.Entities;
using OShop.Infrastructures.Persistence.Contexts;

namespace OShop.Infrastructures.Persistence.Respositories.CategoryRepositories
{
    public class CacheCategoryReadRepository : ICategoryReadRepository
    {
        private readonly CategoryReadRepository _decorated;
        private readonly IDistributedCache _distributedCache;
        private readonly ApplicationDbContext _context;

        public CacheCategoryReadRepository(CategoryReadRepository decorated, IDistributedCache distributedCache, ApplicationDbContext context)
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

    }
}
