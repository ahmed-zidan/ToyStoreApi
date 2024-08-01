using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToyStore.Core.Models.Orders;

namespace ToyStore.Core.IRepository
{
    public interface IDeliveryMethodRepo
    {
        Task<IEnumerable<DeliveryMethod>> getDeliveryMethodsAsync();
        Task<DeliveryMethod> getDeliveryMethodAsync(int deliveryMethodId);
    }
}
