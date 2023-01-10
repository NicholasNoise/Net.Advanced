using Ardalis.Specification;

namespace Net.Advanced.Core.CatalogAggregate.Specifications;

public class CategoryByIdSpec : Specification<Category>, ISingleResultSpecification
{
  public CategoryByIdSpec(int categoryId)
  {
    Query
        .Where(category => category.Id == categoryId);
  }
}
