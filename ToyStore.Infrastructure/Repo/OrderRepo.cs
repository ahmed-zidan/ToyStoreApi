using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToyStore.Core.IRepository;
using ToyStore.Core.Models;
using ToyStore.Core.Models.Orders;
using ToyStore.Infrastructure.Data;

namespace ToyStore.Infrastructure.Repo
{
    public class OrderRepo : IOrderRepo
    {
        private readonly AppDbContext _context;
        public OrderRepo(AppDbContext appContext)
        {
            _context = appContext;
           
        }
        
        public async Task<Order> AddOrderAsync(Basket basket , DeliveryMethod deliveryMethod, ShipAddress address , string buyerEmail)
        {
            var orderItemss = new List<OrderItem>();
            foreach (var item in basket.BasketItem) {
                var product = await _context.Products.FirstOrDefaultAsync(x => x.Id == item.Id);
                orderItemss.Add(new OrderItem { 
                    ProductItemOrder = new ProductItemOrder()
                    {
                        ProductId = product.Id,
                        PictureUrl = product.PictureUrl,
                        ProductName = product.Name,
                    },
                    Price = item.Price,
                    quantity = item.Quantity
                
                });
            }

            var order = new Order()
            {
                BuyerEmail = buyerEmail,
                DeliveryMethod = deliveryMethod,
                OrderDate = DateTime.Now,
                orderItems = orderItemss,
                PaymentIntentId = "",
                ShipToAddress = address,
                SubTotal = orderItemss.Sum(x => x.Price * x.quantity),
            };
            _context.Orders.Add(order);
            return order;
        }

       
        public async Task<Order> getOrderByIdAsync(int orderId, string userEmail)
        {
            return await _context.Orders.Include(x=>x.orderItems).Include(x=>x.DeliveryMethod).FirstOrDefaultAsync(x => x.BuyerEmail == userEmail && x.Id == orderId);
        }

        public async Task<IEnumerable<Order>> getOrdersAync(string userEmail)
        {
            return await _context.Orders.Include(x => x.orderItems).Include(x => x.DeliveryMethod).Where(x => x.BuyerEmail == userEmail).ToListAsync();
        }
    }
}
