using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToyStore.Core.Models;

namespace ToyStore.Core.IRepository
{
    public interface IBasketService
    {
        Task<Basket> getBasket(string basketId);
        Task<Basket> updateBasket(Basket customerBasket);
        Task<bool> deleteBasket(string basketId);
    }
}
