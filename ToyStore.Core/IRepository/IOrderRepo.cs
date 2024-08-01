
using System.Reflection;
using ToyStore.Core.Models;
using ToyStore.Core.Models.Orders;

namespace ToyStore.Core.IRepository
{
    public interface IOrderRepo
    {
        Task<Order> AddOrderAsync(Basket basket, DeliveryMethod deliveryMethod ,ShipAddress address, string buyerEmail);
        Task<IEnumerable<Order>> getOrdersAync(string userEmail);
        Task<Order> getOrderByIdAsync(int orderId , string userEmail);
    }
}
