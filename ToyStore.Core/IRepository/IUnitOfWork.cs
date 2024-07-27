using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToyStore.Core.IRepository
{
    public interface IUnitOfWork
    {
        public IProductRepo _productRepo { get;}
        public ICategoryRepo _categoryRepo { get;}
        Task<bool> saveChangesAsync();
    }
}
