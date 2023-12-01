using OShop.Domain.Abstracts.Repositories.ProductRepositories;
using OShop.Domain.Entities;
using OShop.Infrastructures.Persistence.Contexts;
using OShop.Infrastructures.Persistence.Respositories.BaseRepositories;

namespace OShop.Infrastructures.Persistence.Respositories.ProductRepositories
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {

        public ProductRepository(ApplicationDbContext context) : base(context)
        {
        }

    }
}
