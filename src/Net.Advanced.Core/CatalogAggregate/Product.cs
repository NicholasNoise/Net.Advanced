using Ardalis.GuardClauses;
using Net.Advanced.SharedKernel;
using Net.Advanced.SharedKernel.Interfaces;

namespace Net.Advanced.Core.CatalogAggregate;
public class Product : EntityBase, IAggregateRoot
{
  public Product(string name, decimal price, uint amount)
  {
    Name = Guard.Against.NullOrEmpty(name, nameof(name));
    Price = price;
    Amount = amount;
  }

  public string Name { get; private set; }
  public string? Description { get; set; }
  public string? Image { get; set; }
  public Category? Category { get; set; }
  public decimal Price { get; set; }
  public uint Amount { get; set; }

  public void UpdateName(string newName)
  {
    Name = Guard.Against.NullOrEmpty(newName, nameof(newName));
  }
}
