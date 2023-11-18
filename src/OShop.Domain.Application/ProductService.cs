using OShop.Domain.Abstracts.Application;
using OShop.Domain.Abstracts.Repositories.ProductRepositories;
using OShop.Domain.Entities;

namespace OShop.Domain.Application
{
    public class ProductService : IProductService
    {

        private readonly IProductReadRepository _productReadRepository;
        private readonly IProductWriteRepository _productWriteRepository;
        public ProductService(IProductReadRepository productReadRepository, IProductWriteRepository productWriteRepository)
        {
            _productReadRepository = productReadRepository;
            _productWriteRepository = productWriteRepository;
        }



        public async Task<List<Product>> GetAllProductsByCategory(int categoryId)
        {
            try
            {
                var result = await _productReadRepository.GetAllAsync();
                return result.Where(x => x.CategoryId == categoryId).ToList();
            }
            catch
            {
                throw;
            }
        }


        public async Task<Product?> GetProductById(int id)
        {
            try
            {
                return await _productReadRepository.FindByIdAsync(id);
            }
            catch
            {
                throw;
            }
        }

        public async Task InsertProduct(Product product)
        {
            try
            {
                await _productWriteRepository.AddAsync(product);
            }
            catch
            {
                throw;
            }
        }

        public async Task UpdateProduct(int id, Product product)
        {
            try
            {
                await _productWriteRepository.UpdateAsync(product);
            }
            catch
            {
                throw;
            }
        }

        public async Task DeleteProduct(int id)
        {
            try
            {
                var product = await GetProductById(id);
                if (product != null)
                {
                    await _productWriteRepository.RemoveAsync(product);
                }
            }
            catch
            {
                throw;
            }
        }
    }
}
