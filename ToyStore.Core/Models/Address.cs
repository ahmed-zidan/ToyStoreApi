using System.ComponentModel.DataAnnotations.Schema;

namespace ToyStore.Core.Models
{
    public class Address:BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ZipCode { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        [ForeignKey("AppUser")]
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }

    }
}