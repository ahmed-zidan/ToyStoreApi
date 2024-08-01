using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToyStore.Core.Models.Orders;

namespace ToyStore.Infrastructure.Data.Config
{
    public class DeliveryMethodConfiguration : IEntityTypeConfiguration<DeliveryMethod>
    {
        public void Configure(EntityTypeBuilder<DeliveryMethod> builder)
        {
            builder.Property(x => x.Price).HasColumnType("decimal(18,2)");
            builder.HasData(
                new DeliveryMethod() { Id = 1, Price = 20,DeliveryTime="", Description = "Fastest Delivey", ShortName = "DHL" },
                new DeliveryMethod() { Id = 2, Price = 30, DeliveryTime = "", Description = "Git it with 2 days", ShortName = "Fedex" },
                new DeliveryMethod() { Id = 3, Price = 40, DeliveryTime = "", Description = "Slower but cheap", ShortName = "Armax" },
                new DeliveryMethod() { Id = 4, Price = 0, DeliveryTime = "", Description = "Free", ShortName = "Jumia" }


                );
        }
    }
}
