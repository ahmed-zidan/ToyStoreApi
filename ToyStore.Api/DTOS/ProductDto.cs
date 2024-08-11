using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ToyStore.Core.Models;

namespace ToyStore.Api.DTOS
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal MainPrice { get; set; }
        public decimal SellPrice { get; set; }
        public bool IsSale { get; set; }
        public bool IsNew { get; set; }
        public int CategotyId { get; set; }
        public string CategoryName { get; set; }
        public string ImageUrl { get; set; }
        public List<ColorDto> Colors { get; set; }
        public List<ListOFIdAndName> Sizes { get; set; }
    }
}
