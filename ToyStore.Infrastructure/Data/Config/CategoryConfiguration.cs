﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToyStore.Core.Models;

namespace ToyStore.Infrastructure.Data.Config
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.Property(x=>x.Name).HasMaxLength(50);
            builder.Property(x=>x.Description).HasMaxLength(50);
            builder.HasData(
                new Category { Id = 1, Name = "Category 1", Description = "description 1" },
                new Category { Id = 2, Name = "Category 2", Description = "description 2" },
                new Category { Id = 3, Name = "Category 3", Description = "description 3" }
                );
        }
    }
}
