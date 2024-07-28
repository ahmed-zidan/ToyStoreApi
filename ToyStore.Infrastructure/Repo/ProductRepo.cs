using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToyStore.Core.IRepository;
using ToyStore.Core.Models;
using ToyStore.Infrastructure.Data;

namespace ToyStore.Infrastructure.Repo
{
    public class ProductRepo : IProductRepo
    {
        private readonly AppDbContext _context;
        public ProductRepo(AppDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Product model)
        {
            await _context.Products.AddAsync(model);
        }

        public void Delete(Product model)
        {
            _context.Products.Remove(model);
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await _context.Products.Include(x=>x.Category).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Product> GetProductByNameAsync(string Name)
        {
            return await _context.Products.Include(x=>x.Category).FirstOrDefaultAsync(x => x.Name == Name);
        }

        public async Task<IEnumerable<Product>> GetProductsAsync(int categoryId)
        {
            return await _context.Products.Include(x=>x.Category).Where(x=>x.CategotyId == categoryId).ToListAsync();    
        }

        public void Update(Product model)
        {
            _context.Products.Update(model);
        }
    }
}
