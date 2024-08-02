using ToyStore.Core.Models.Orders;

namespace ToyStore.Api.DTOS
{
    public class OrderToReturnDto
    {
        public int Id { get; set; }
        public string BuyerEmail { get; set; }
        public DateTimeOffset OrderDate { get; set; }
        public AddressDto ShipToAddress { get; set; }
        public DeliveryMethod DeliveryMethod { get; set; }
        public ICollection<OrderItemToReturnDto> orderItems { get; set; }
        public decimal SubTotal { get; set; }
        public string Status { get; set; }
        public string PaymentIntentId { get; set; }
        public decimal Total { get; set; }
    }
}
