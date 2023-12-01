using OShop.Domain.Entities;

namespace OShop.Domain.Abstracts.ApplicationServices
{
    public interface IProductService
    {
        Task<Product> GetProductByIdAsync(int id);
        Task<IEnumerable<Product>> GetProductsAsync(int categoryId);

        Task InsertProductAsync(Product product);
        Task UpdateProductAsync(Product product);
        Task DeleteProductAsync(int id);
    }
}
