using System.ComponentModel.DataAnnotations;
using ToyStore.Core.Models;

namespace ToyStore.Api.DTOS
{
    public class AddProductDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public decimal MainPrice { get; set; }
        [Required]
        public decimal SellPrice { get; set; }
        [Required]
        public int CategotyId { get; set; }
        public IFormFile Image { get; set; }
        public List<int> ColorId { get; set; }
        public List<int> SizeId { get; set; }
        public bool IsSale { get; set; }
        public bool IsNew { get; set; }
    }
}
