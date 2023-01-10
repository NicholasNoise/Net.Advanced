using Ardalis.GuardClauses;
using Net.Advanced.SharedKernel;
using Net.Advanced.SharedKernel.Interfaces;

namespace Net.Advanced.Core.CatalogAggregate;
public class Product : EntityBase, IAggregateRoot
{
  public Product(string name, Category category)
  {
    Name = Guard.Against.NullOrEmpty(name, nameof(name));
    Category = category;
  }

  public string Name { get; private set; }
  public string? Description { get; set; } = string.Empty;
  public string? Image { get; set; } = string.Empty;
  public Category Category { get; set; }
  public decimal Price { get; set; }
  public uint Amount { get; set; }

  public void UpdateName(string newName)
  {
    Name = Guard.Against.NullOrEmpty(newName, nameof(newName));
  }
}
