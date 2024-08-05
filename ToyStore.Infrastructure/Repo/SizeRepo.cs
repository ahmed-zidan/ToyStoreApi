using Abp.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ToyStore.Core.IRepository;
using ToyStore.Core.Models;
using ToyStore.Infrastructure.Data;

namespace ToyStore.Infrastructure.Repo
{
    public class SizeRepo : ISizeRepo
    {
        private readonly AppDbContext _context;
        public SizeRepo(AppDbContext context) {
            _context = context;
        }
        public async Task AddSizeAsync(Size size)
        {
            await _context.Sizes.AddAsync(size);
        }

        public async Task<IEnumerable<Size>> GetSizesAsync()
        {
            return await _context.Sizes.ToListAsync();
        }

        public void RemoveSizeAsync(Size size)
        {
            _context.Sizes.Remove(size);
        }
        public async Task<bool> IsExist(string name)
        {
            return await _context.Colors.AnyAsync(c => c.Name == name);
        }

        public async Task<Size> Get(int id)
        {
            return await _context.Sizes.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Size>> GetSizesAsync(List<int> Ids)
        {
            Expression<Func<Size, bool>> predicate = PredicateBuilder.False<Size>(); ;
            foreach (var color in Ids)
            {
                predicate = predicate.Or(x => x.Id == color);
            }
            return await _context.Sizes.Where(predicate).ToListAsync();
        }
    }
}
