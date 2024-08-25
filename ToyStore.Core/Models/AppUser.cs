using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToyStore.Core.Models
{
    public class AppUser:IdentityUser
    {
        public string DisplayName { get; set; }
        public Address? Address { get; set; }
        public string Role { get; set; }
        public string? Photo { get; set; }
    }
}
