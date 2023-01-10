using Ardalis.GuardClauses;
using Net.Advanced.SharedKernel;
using Net.Advanced.SharedKernel.Interfaces;

namespace Net.Advanced.Core.CatalogAggregate;
public class Category : EntityBase, IAggregateRoot
{
  public Category(string name)
  {
    Name = Guard.Against.NullOrEmpty(name, nameof(name));
  }

  public string Name { get; private set; }
  public string? Image { get; private set; }
  public Category? Parent { get; private set; }

  public void UpdateName(string newName)
  {
    Name = Guard.Against.NullOrEmpty(newName, nameof(newName));
  }

  public void UpdateImage(string? newImage)
  {
    Image = newImage;
  }

  public void UpdateParent(Category? parent)
  {
    Parent = parent;
  }
}
