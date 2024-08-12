using System.ComponentModel.DataAnnotations;
using ToyStore.Core.Models;

namespace ToyStore.Api.DTOS
{
    public class UserDto
    {
        [Required]
        public string Id { get; set; }
        [Required]
        public string DisplayName { get; set; }
        [Required]
        public string Role { get; set; }
        public string? PhoneNumber { get; set; }
        public IFormFile Photo { get; set; }
    }
}
