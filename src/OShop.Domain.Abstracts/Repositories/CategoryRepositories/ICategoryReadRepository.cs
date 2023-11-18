using OShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OShop.Domain.Abstracts.Repositories.CategoryRepositories
{
    public interface ICategoryReadRepository
    {
        Task<Category?> FindByIdAsync(int id);
        Task<IEnumerable<Category>> GetAllAsync(params string[] IncludeProperties);
    }
}
