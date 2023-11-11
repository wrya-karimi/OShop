using Data.Respositories.CategoryRepository;
using Domain.Entities;


namespace Services.Categories
{
    public class CategoryService: ICategoryService
    {

        private ICategoryRepository _repository;

        public CategoryService(ICategoryRepository repository)
        {
            _repository = repository;
        }


        public async Task<List<Category>> GetAll()
        {
            try
            {
                 var result = await _repository.GetAllAsync();
                return result.ToList();
            }
            catch
            {
                throw;
            }
        }


        public async Task<Category> GetCategoryById(int id)
        {
            try
            {
                return await _repository.FindByIdAsync(id);
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
                await _repository.AddAsync(category);
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
                await _repository.UpdateAsync(category);
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
                var category = await _repository.FindByIdAsync(id);
                if (category != null)
                {
                    await _repository.RemoveAsync(category);
                }
            }
            catch
            {
                throw;
            }
        }
    }
}
