using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Net.Advanced.Core.CatalogAggregate;

namespace Net.Advanced.Infrastructure.Data.Config;
public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
  /// <inheritdoc/>
  public void Configure(EntityTypeBuilder<Category> builder)
  {
    builder.Property(p => p.Name)
      .HasMaxLength(100)
      .IsRequired();
  }
}
