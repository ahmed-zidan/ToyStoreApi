using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToyStore.Core.Models;

namespace ToyStore.Infrastructure.Data.Config
{
    public class MenuConfiguration : IEntityTypeConfiguration<Menu>
    {
        public void Configure(EntityTypeBuilder<Menu> builder)
        {
            builder.HasData(
                new Menu() { Id = 1, Name = "Home",UserName= "Home", Status = true },
                new Menu() { Id = 2, Name = "Product", UserName = "Product", Status = true },
                new Menu() { Id = 3, Name = "Category", UserName = "Category", Status = true },
                new Menu() { Id = 4, Name = "User Manager", UserName = "UserManager", Status = true },
                new Menu() { Id = 5, Name = "Role Manager", UserName = "RoleManager", Status = true },
                new Menu() { Id = 6, Name = "Menu Manager", UserName = "MenuManager", Status = true }
                );

        }
    }
}
