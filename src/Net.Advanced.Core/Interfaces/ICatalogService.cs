using Net.Advanced.Core.CartAggregate;

namespace Net.Advanced.Core.Interfaces;
internal interface ICatalogService
{
  Task<Product?> GetProductById(int id);

  Task<IAsyncEnumerable<Product>> GetProducts(int id);

  Task SaveProduct(Product product);

  Task DeleteProduct(Product product);

  Task<Category?> GetCategoryById(int id);

  Task<IAsyncEnumerable<Category>> GetCategories(int id);

  Task SaveCategory(Category category);

  Task DeleteCategory(Category category);
}
