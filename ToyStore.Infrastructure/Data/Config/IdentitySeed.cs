using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using ToyStore.Core.Models;

namespace ToyStore.Infrastructure.Data.Config
{
    public class IdentitySeed
    {
        public static async Task SeedUserAsync(UserManager<AppUser> userManager,RoleManager<IdentityRole>roleManager)
        {
            if (!userManager.Users.Any())
            {
                var user = new AppUser()
                {
                    DisplayName = "Zidan Zezo",
                    Email = "Zidan@gmail.com",
                    UserName = "ZidanZezo",
                    PhoneNumber = "01115930826",
                    Role = "Admin",
                    Address = new Address()
                    {
                        City = "Giza",
                        Country = "Egypt",
                        FirstName = "fName",
                        LastName = "LName",
                        Street = "Mo Street",
                        ZipCode = "1112"
                    },
                };

                await userManager.CreateAsync(user,"zidan@123");
                await roleManager.CreateAsync(new IdentityRole("Admin"));
                await roleManager.CreateAsync(new IdentityRole("User"));
                await userManager.AddToRoleAsync(user, "Admin");
            }
        }
    }
}
