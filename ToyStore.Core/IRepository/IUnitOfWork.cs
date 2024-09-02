using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToyStore.Core.IRepository
{
    public interface IUnitOfWork
    {
        public IMenuRepo _menuRepo{ get;}
        public IProductRepo _productRepo { get;}
        public ICategoryRepo _categoryRepo { get;}
        public IOrderRepo _orderRepo { get;}
        public IDeliveryMethodRepo _deliveryRepo { get;}
        public IColorRepo _colorRepo{ get;}
        public ISizeRepo _sizeRepo { get;}
        public IPaginationRepo _paginationRepo { get;}
        Task<bool> saveChangesAsync();
    }
}
