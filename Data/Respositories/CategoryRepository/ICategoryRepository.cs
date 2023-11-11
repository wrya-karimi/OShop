using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Respositories.CategoryRepository
{
    public interface ICategoryRepository
    {
        Task<Category?> FindByIdAsync(int id);
        Task AddAsync(Category category);
        Task UpdateAsync(Category category);
        Task<IEnumerable<Category>> GetAllAsync(params string[] IncludeProperties);
        Task RemoveAsync(Category category);
    }
}
