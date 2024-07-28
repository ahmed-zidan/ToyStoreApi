using System.ComponentModel.DataAnnotations.Schema;
using ToyStore.Core.Models;

namespace ToyStore.Api.DTOS
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int CategotyId { get; set; }
        public string CategoryName { get; set; }
        public string ImageUrl { get; set; }
    }
}
