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
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.OwnsOne(x => x.ShipToAddress, n => { n.WithOwner(); });
            builder.Property(x=>x.Status).HasConversion(x=>x.ToString(),x=>(OrderStatus)Enum.Parse(typeof(OrderStatus) , x));
            builder.HasMany(x => x.orderItems).WithOne().OnDelete(DeleteBehavior.Cascade);
            builder.Property(x => x.SubTotal).HasColumnType("decimal(18,2)");
        }
    }
}
