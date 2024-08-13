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
                new Menu() { Id = 1, Name = "Home", Status = true },
                new Menu() { Id = 2, Name = "Product", Status = true },
                new Menu() { Id = 3, Name = "Category", Status = true },
                new Menu() { Id = 4, Name = "User Manager", Status = true },
                new Menu() { Id = 5, Name = "Role Manager", Status = true },
                new Menu() { Id = 6, Name = "Menu Manager", Status = true }
                );

        }
    }
}
