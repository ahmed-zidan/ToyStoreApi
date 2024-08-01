using ToyStore.Core.Models.Orders;

namespace ToyStore.Api.DTOS
{
    public class AddOrderDto
    {
        public string basketId { get; set; }
        public int deliveryMethode { get; set; }
        public ShipAddressDto shippingAddress { get; set; }
    }
}
