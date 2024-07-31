using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToyStore.Core.Models.Orders
{
    public class Order:BaseEntity
    {
        public string BuyerEmail { get; set; }
        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;
        public ShipAddress ShipToAddress { get; set; }
        public DeliveryMethod DeliveryMethod { get; set; }
        public ICollection<OrderItem> orderItems { get; set; }
        public decimal SubTotal { get; set; }
        public OrderStatus Status { get; set; } = OrderStatus.Pending;
        public string PaymentIntentId { get; set; }

        public decimal getTotal()
        {
            return this.SubTotal + DeliveryMethod.Price;
        }
    }
}
