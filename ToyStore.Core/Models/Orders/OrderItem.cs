namespace ToyStore.Core.Models.Orders
{
    public class OrderItem:BaseEntity
    {
        public ProductItemOrder ProductItemOrder { get; set; }
        public decimal Price { get; set; }
        public int quantity { get; set; }
    }
}