namespace ToyStore.Core.Models
{
    public class Size:BaseEntity
    {
        public string Name { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}