using OShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OShop.Domain.Abstracts.Repositories.CategoryRepositories
{
    public interface ICategoryWriteRepository
    {
        Task AddAsync(Category category);
        Task UpdateAsync(Category category);
        Task RemoveAsync(Category category);
    }
}
