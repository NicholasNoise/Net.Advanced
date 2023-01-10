﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Net.Advanced.Core.CatalogAggregate;

namespace Net.Advanced.Infrastructure.Data.Config;
public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
  public void Configure(EntityTypeBuilder<Product> builder)
  {
    builder.Property(p => p.Name)
      .HasMaxLength(100)
      .IsRequired();

    builder.HasOne(p => p.Category)
      .WithMany()
      .OnDelete(DeleteBehavior.Restrict);
  }
}