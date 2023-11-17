using Data.Respositories.ProductRepository;
using Domain.Entities;


namespace Services.Products
{
    public class ProductService: IProductService
    {

        private IProductRepository _repository;

        public ProductService(IProductRepository repository)
        {
            _repository = repository;
        }

        //public IRepository<Product> Get_repository()
        //{
        //    return _repository;
        //}


        public async Task<List<Product>> GetAllProductsByCategory(int categoryId)
        {
            try
            {
                 var result = await _repository.GetAllAsync();
                 return result.Where(x=> x.CategoryId == categoryId).ToList();
            }
            catch
            {
                throw;
            }
        }


        public async Task<Product> GetProductById(int id)
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

        public async Task InsertProduct(Product product)
        {
            //try
            //{
                await _repository.AddAsync(product);
            //}
            //catch
            //{
            //    throw;
            //}
        }

        public async Task UpdateProduct(int id, Product product)
        {
            try
            {
                await _repository.UpdateAsync(product);
            }
            catch
            {
                throw;
            }
        }

        public async Task DeleteProduct(int id)
        {
            try
            {
                var product = await _repository.FindByIdAsync(id);
                if (product != null)
                {
                    await _repository.RemoveAsync(product);
                }
            }
            catch
            {
                throw;
            }
        }
    }
}
