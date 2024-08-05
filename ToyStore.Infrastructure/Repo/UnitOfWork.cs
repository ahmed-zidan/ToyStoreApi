using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToyStore.Core.IRepository;
using ToyStore.Infrastructure.Data;

namespace ToyStore.Infrastructure.Repo
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        public UnitOfWork(AppDbContext context) {
            _context = context; 
        }

        public IProductRepo _productRepo => new ProductRepo(_context);

        public ICategoryRepo _categoryRepo => new CategoryRepo(_context);

        public IOrderRepo _orderRepo => new OrderRepo(_context);

        public IDeliveryMethodRepo _deliveryRepo => new DeliveryMethodRepo(_context);

        public IColorRepo _colorRepo => new ColorRepo(_context);

        public ISizeRepo _sizeRepo => new SizeRepo(_context);

        public async Task<bool> saveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
