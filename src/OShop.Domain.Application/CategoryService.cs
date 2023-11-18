using OShop.Domain.Abstracts.Application;
using OShop.Domain.Abstracts.Repositories.CategoryRepositories;
using OShop.Domain.Entities;

namespace OShop.Domain.Application
{
    public class CategoryService : ICategoryService
    {

        private readonly ICategoryReadRepository _categoryReadRepository;
        private readonly ICategoryWriteRepository _categoryWriteRepository;
        public CategoryService(ICategoryReadRepository categoryReadRepository, ICategoryWriteRepository categoryWriteRepository)
        {
            _categoryReadRepository = categoryReadRepository;
            _categoryWriteRepository = categoryWriteRepository;
        }

        public async Task<Category?> GetCategoryById(int id)
        {
            try
            {
                return await _categoryReadRepository.FindByIdAsync(id);
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<Category>> GetAll()
        {
            try
            {
                var result = await _categoryReadRepository.GetAllAsync();
                return result.ToList();
            }
            catch
            {
                throw;
            }
        }

        public async Task InsertCategory(Category category)
        {
            try
            {
                await _categoryWriteRepository.AddAsync(category);
            }
            catch
            {
                throw;
            }
        }

        public async Task UpdateCategory(int id, Category category)
        {
            try
            {
                await _categoryWriteRepository.UpdateAsync(category);
            }
            catch
            {
                throw;
            }
        }

        public async Task DeleteCategory(int id)
        {
            try
            {
                var category = await GetCategoryById(id);
                if (category != null)
                {
                    await _categoryWriteRepository.RemoveAsync(category);
                }
            }
            catch
            {
                throw;
            }
        }
    }
}
