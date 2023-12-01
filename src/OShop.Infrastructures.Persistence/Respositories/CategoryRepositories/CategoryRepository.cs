using OShop.Domain.Abstracts.Repositories.CategoryRepositories;
using OShop.Domain.Entities;
using OShop.Infrastructures.Persistence.Contexts;
using OShop.Infrastructures.Persistence.Respositories.BaseRepositories;

namespace OShop.Infrastructures.Persistence.Respositories.CategoryRepositories
{
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {

        public CategoryRepository(ApplicationDbContext context) : base(context)
        {
        }

    }
}
