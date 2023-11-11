using Domain.Entities;

namespace Services.Products
{
    public interface IProductService
    {
        Task<List<Product>> GetAllProductsByCategory(int categoryId);
        Task<Product?> GetProductById(int id);
        Task InsertProduct(Product product);
        Task DeleteProduct(int id);
        Task UpdateProduct(int id, Product product);
    }
}
