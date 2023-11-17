using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using OShop.Domain.Abstracts.Repositories.ProductRepositories;
using OShop.Domain.Entities;
using OShop.Infrastructures.Persistence.Contexts;

namespace OShop.Infrastructures.Persistence.Respositories.ProductRepositories
{
    public class CacheProductReadRepository : IProductReadRepository
    {
        private readonly ProductReadRepository _decorated;
        private readonly IDistributedCache _distributedCache;
        private readonly ApplicationDbContext _context;

        public CacheProductReadRepository(ProductReadRepository decorated, IDistributedCache distributedCache, ApplicationDbContext context)
        {
            _decorated = decorated;
            _distributedCache = distributedCache;
            _context = context;
        }

        public async Task<Product?> FindByIdAsync(int id)
        {
            string key = $"product{id}";

            string? cachedProduct = await _distributedCache.GetStringAsync(key);
            Product? product;
            if (string.IsNullOrEmpty(cachedProduct))
            {
                product = await _decorated.FindByIdAsync(id);
                if (product is null)
                {
                    return product;
                }

                await _distributedCache.SetStringAsync(key, JsonConvert.SerializeObject(product));
                return product;
            }

            product = JsonConvert.DeserializeObject<Product>(cachedProduct);

            if (product is not null)
            {
                _context.Set<Product>().Attach(product);
            }

            return product;
        }


        public Task<IEnumerable<Product>> GetAllAsync(params string[] IncludeProperties)
        {
            return _decorated.GetAllAsync(IncludeProperties);
        }

    }
}
