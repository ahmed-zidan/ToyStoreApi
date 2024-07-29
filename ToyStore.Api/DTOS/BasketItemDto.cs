using System.ComponentModel.DataAnnotations;

namespace ToyStore.Api.DTOS
{
    public class BasketItemDto
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string ProductName { get; set; }
        [Required]
        [Range(1, double.MaxValue)]
        public decimal Price { get; set; }
        [Required]
        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }
        [Required]
        public string PictureUrl { get; set; }
        [Required]
        public string CategoryId { get; set; }
        [Required]
        public string CategoryName { get; set; }
        
    }
}
