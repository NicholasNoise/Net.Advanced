using Net.Advanced.Core.CatalogAggregate;

namespace Net.Advanced.Web.Endpoints.CategoryEndpoints;

public record CategoryRecord(int Id, string Name, string? Image, CategoryRecord? Parent = null)
{
  public static CategoryRecord FromCategory(Category? category)
  {
    if (category is null)
    {
      throw new ArgumentNullException(nameof(category));
    }

    return new CategoryRecord(
      category.Id,
      category.Name,
      category.Image,
      category.Parent is null ? null : FromCategory(category.Parent));
  }
}
