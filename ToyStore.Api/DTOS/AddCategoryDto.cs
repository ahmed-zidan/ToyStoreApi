using System.ComponentModel.DataAnnotations;
using ToyStore.Core.Models;

namespace ToyStore.Api.DTOS
{
    public class AddCategoryDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
    }
}
