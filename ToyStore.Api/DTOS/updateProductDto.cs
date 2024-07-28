using System.ComponentModel.DataAnnotations;
using ToyStore.Core.Models;

namespace ToyStore.Api.DTOS
{
    public class updateProductDto
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public int CategotyId { get; set; }
        public IFormFile? Image { get; set; }
    }
}
