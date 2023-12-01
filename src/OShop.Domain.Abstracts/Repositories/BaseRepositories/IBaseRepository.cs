using OShop.Domain.Entities;

namespace OShop.Domain.Abstracts.Repositories.BaseRepositories
{
    public interface IBaseRepository<T> where T : BaseEntity, new()
    {
        Task<T> FindByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync(params string[] IncludeProperties);

        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(int id);
    }
}
