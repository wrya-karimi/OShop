using OShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OShop.Domain.Abstracts.Application
{
    public interface ICategoryService
    {
        Task<List<Category>> GetAll();
        Task<Category?> GetCategoryById(int id);
        Task InsertCategory(Category category);
        Task DeleteCategory(int id);
        Task UpdateCategory(int id, Category category);
    }
}
