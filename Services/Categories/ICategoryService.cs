using Domain.Entities;

namespace Services.Categories
{
    public interface ICategoryService
    {
        Task<List<Category>> GetAll();
        Task<Category?> GetCategoryById(int id);
        Task InsertCategory(Category category);
        Task DeleteCategory(int id);
        Task UpdateCategory(int id, Category category);
    }
}
