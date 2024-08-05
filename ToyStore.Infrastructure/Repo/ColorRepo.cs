using Abp.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;
using ToyStore.Core.IRepository;
using ToyStore.Core.Models;
using ToyStore.Infrastructure.Data;

namespace ToyStore.Infrastructure.Repo
{
    public class ColorRepo : IColorRepo
    {
        private readonly AppDbContext _context;
        public ColorRepo(AppDbContext context)
        {
            _context = context;
        }
        public async Task AddColorAsync(Color color)
        {
            await _context.Colors.AddAsync(color);
        }

        public async Task<Color> Get(int id)
        {
            return await _context.Colors.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Color>> GetColorsAsync()
        {
            return await _context.Colors.ToListAsync();
        }

        public async Task<IEnumerable<Color>> GetColorsAsync(List<int>Ids)
        {
            Expression<Func<Color, bool>> predicate = PredicateBuilder.False<Color>(); ;
            foreach (var color in Ids) {
                predicate = predicate.Or(x => x.Id == color);
            }
            return await _context.Colors.Where(predicate).ToListAsync();
        }

        public async Task<bool> IsExist(string name)
        {
            return await _context.Colors.AnyAsync(c => c.Name == name);
        }

        public void RemoveColorAsync(Color color)
        {
            _context.Colors.Remove(color);
        }
    }
}
