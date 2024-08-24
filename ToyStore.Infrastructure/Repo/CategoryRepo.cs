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
    public class CategoryRepo : ICategoryRepo
    {
        private readonly AppDbContext _context;
        public CategoryRepo(AppDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Category model)
        {
            await _context.Categories.AddAsync(model);
        }

        public void Delete(Category model)
        {
            _context.Categories.Remove(model);
        }

        public async Task<IEnumerable<Category>> GetCategoriesAsync()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<IEnumerable<Object>> GetCategoriesNameAndId()
        {
            return await _context.Categories.Select(x => new { Name = x.Name, Id = x.Id }).ToListAsync();
        }

        public async Task<Category> GetCategoryByIdAsync(int id)
        {
            return await _context.Categories.FirstOrDefaultAsync(x=>x.Id == id);
        }

        public async Task<Category> GetCategoryByNameAsync(string Name)
        {
            return await _context.Categories.FirstOrDefaultAsync(x => x.Name == Name);
        }

        public void Update(Category model)
        {
            _context.Categories.Update(model);
        }
    }
}
