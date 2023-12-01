using OShop.Domain.Abstracts.ApplicationServices;
using OShop.Domain.Abstracts.Repositories.ProductRepositories;
using OShop.Domain.Entities;

namespace OShop.Domain.ApplicationServices
{
    public class ProductService : IProductService
    {

        private readonly IProductRepository _productRepository;
        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            try
            {
                return await _productRepository.FindByIdAsync(id);
            }
            catch
            {
                throw;
            }
        }

        public async Task<IEnumerable<Product>> GetProductsAsync(int categoryId)
        {
            try
            {
                var result = await _productRepository.GetAllAsync();
                return result.Where(x => x.CategoryId == categoryId).ToList();
            }
            catch
            {
                throw;
            }
        }

        public async Task InsertProductAsync(Product product)
        {
            try
            {
                await _productRepository.AddAsync(product);
            }
            catch
            {
                throw;
            }
        }

        public async Task UpdateProductAsync(Product product)
        {
            try
            {
                await _productRepository.UpdateAsync(product);
            }
            catch
            {
                throw;
            }
        }

        public async Task DeleteProductAsync(int id)
        {
            try
            {
                await _productRepository.DeleteAsync(id);
            }
            catch
            {
                throw;
            }
        }
    }
}
