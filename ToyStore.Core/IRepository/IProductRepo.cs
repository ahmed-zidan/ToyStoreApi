using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToyStore.Core.Models;

namespace ToyStore.Core.IRepository
{
    public interface IProductRepo
    {
        Task AddAsync(Product model);
        Task<IEnumerable<Product>> GetProductsAsync(int categoryId);
        Task<Product> GetProductByIdAsync(int id);
        Task<Product> GetProductByNameAsync(string Name);
        void Update(Product model);
        void Delete(Product models);
    }
}
