using OShop.Domain.Entities;

namespace OShop.Domain.Abstracts.ApplicationServices
{
    public interface ICategoryService
    {
        Task<Category> GetCategoryByIdAsync(int id);
        Task<IEnumerable<Category>> GetCategoriesAsync(params string[] IncludeProperties);

        Task InsertCategoryAsync(Category category);
        Task UpdateCategoryAsync(Category category);
        Task DeleteCategoryAsync(int id);
    }
}
