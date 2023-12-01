using OShop.Domain.Abstracts.ApplicationServices;
using OShop.Domain.Abstracts.Repositories.CategoryRepositories;
using OShop.Domain.Entities;

namespace OShop.Domain.ApplicationServices
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<Category> GetCategoryByIdAsync(int id)
        {
            try
            {
                return await _categoryRepository.FindByIdAsync(id);
            }
            catch
            {
                throw;
            }
        }

        public async Task<IEnumerable<Category>> GetCategoriesAsync(params string[] IncludeProperties)
        {
            try
            {
                var result = await _categoryRepository.GetAllAsync();
                return result.ToList();
            }
            catch
            {
                throw;
            }
        }

        public async Task InsertCategoryAsync(Category category)
        {
            try
            {
                await _categoryRepository.AddAsync(category);
            }
            catch
            {
                throw;
            }
        }

        public async Task UpdateCategoryAsync(Category category)
        {
            try
            {
                await _categoryRepository.UpdateAsync(category);
            }
            catch
            {
                throw;
            }
        }

        public async Task DeleteCategoryAsync(int id)
        {
            try
            {
                await _categoryRepository.DeleteAsync(id);
            }
            catch
            {
                throw;
            }
        }
    }
}
