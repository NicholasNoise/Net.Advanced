using Net.Advanced.Core.CatalogAggregate;
using Net.Advanced.Web.Endpoints.CategoryEndpoints;

namespace Net.Advanced.Web.Endpoints.ProductEndpoints;

public record ProductRecord(int Id, string Name, string? Description, string? Image, decimal Price, uint Amount, CategoryRecord Category)
{
  public static ProductRecord FromProduct(Product? product)
  {
    if (product is null)
    {
      throw new ArgumentNullException(nameof(product));
    }

    return new ProductRecord(
      product.Id,
      product.Name,
      product.Description,
      product.Image,
      product.Price,
      product.Amount,
      CategoryRecord.FromCategory(product.Category));
  }
};
