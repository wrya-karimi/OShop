using Domain.Entities;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Respositories.ProductRepository
{
    public class CacheProductRepository : IProductRepository
    {
        private readonly ProductRepository _decorated;
        private readonly IDistributedCache _distributedCache;
        private readonly ApplicationDbContext _context;

        public CacheProductRepository(ProductRepository decorated, IDistributedCache distributedCache, ApplicationDbContext context)
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

                await _distributedCache.SetStringAsync(key,JsonConvert.SerializeObject(product));
                return product;
            }

            product = JsonConvert.DeserializeObject<Product>(cachedProduct);

            if(product is not null)
            {
                _context.Set<Product>().Attach(product);
            }

            return product;
        }

        public Task AddAsync(Product product)
        {
            return _decorated.AddAsync(product);
        }

        public Task<IEnumerable<Product>> GetAllAsync(params string[] IncludeProperties)
        {
            return _decorated.GetAllAsync(IncludeProperties);
        }

        public Task RemoveAsync(Product product)
        {
            return _decorated.RemoveAsync(product);
        }

        public Task UpdateAsync(Product product)
        {
            return _decorated.UpdateAsync(product);
        }
    }
}
