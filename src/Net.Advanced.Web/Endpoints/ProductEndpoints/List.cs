using FastEndpoints;
using Net.Advanced.SharedKernel.Interfaces;
using Net.Advanced.Core.CatalogAggregate;

namespace Net.Advanced.Web.Endpoints.ProductEndpoints;

public class List : EndpointWithoutRequest<ProductListResponse>
{
  private readonly IRepository<Product> _repository;

  public const string Route = "/Products";

  public List(IRepository<Product> repository)
  {
    _repository = repository;
  }

  public override void Configure()
  {
    Get(Route);
    AllowAnonymous();
    Options(x => x
      .WithTags("ProductEndpoints"));
  }
  public override async Task HandleAsync(CancellationToken cancellationToken)
  {
    var products = await _repository.ListAsync(cancellationToken);
    var response = new ProductListResponse
    {
      Products = products
        .Select(ProductRecord.FromProduct)
        .ToList()
    };

    await SendAsync(response, cancellation: cancellationToken);
  }
}
