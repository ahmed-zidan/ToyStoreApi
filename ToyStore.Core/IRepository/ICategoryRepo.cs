using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToyStore.Core.Models;

namespace ToyStore.Core.IRepository
{
    public interface ICategoryRepo
    {
        Task AddAsync(Category model);
        Task<IEnumerable<Category>> GetCategoriesAsync();
        Task<Category> GetCategoryByIdAsync(int id);
        Task<Category> GetCategoryByNameAsync(string Name);
        void Update(Category model);
        void Delete(Category category);
        
    }
}
