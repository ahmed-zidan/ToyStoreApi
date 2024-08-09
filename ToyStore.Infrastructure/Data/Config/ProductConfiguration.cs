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
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(x => x.Name).HasMaxLength(128);
            builder.Property(x => x.Description).HasMaxLength(128);
            builder.Property(x => x.SellPrice).HasColumnType("decimal(18,2)");
            builder.Property(x => x.MainPrice).HasColumnType("decimal(18,2)");
            builder.HasData(
                new Product { Id = 1, Name = "Product 1", CategotyId = 1, Description = "Description1", MainPrice = 100,SellPrice= 100,IsNew=true, PictureUrl="http" },
                new Product { Id = 2, Name = "Product 2", CategotyId = 2, Description = "Description2", MainPrice= 200, SellPrice = 200,IsSale=true, PictureUrl = "http" },
                new Product { Id = 3, Name = "Product 3", CategotyId = 3, Description = "Description3", MainPrice = 300, SellPrice = 240, PictureUrl = "http" }
                );
        }
    }
}
