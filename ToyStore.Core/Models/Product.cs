using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToyStore.Core.Models
{
    public class Product:BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal MainPrice { get; set; }
        public decimal SellPrice { get; set; }
        public bool IsSale { get; set; }
        public bool IsNew { get; set; }
        public string PictureUrl { get; set; }
        [ForeignKey("Category")]
        public int CategotyId { get; set; }
        public Category Category { get; set; }
        public ICollection<Color> colors { get; set; }
        public ICollection<Size> Sizes { get; set; }
    }
}
