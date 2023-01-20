using FastEndpoints;
using Net.Advanced.Core.CatalogAggregate;
using Net.Advanced.SharedKernel.Interfaces;

namespace Net.Advanced.Web.Endpoints.ProductEndpoints;

public class List : Endpoint<ProductListRequest, ProductListResponse>
{
  private const string Route = "/Products";

  private readonly IRepository<Product> _repository;

  public List(IRepository<Product> repository)
  {
    _repository = repository;
  }

  public static string BuildRoute(int perPage, int page) => $"{Route}?perPage={perPage}&page={page}";

  public override void Configure()
  {
    Get(Route);
    AllowAnonymous();
    Options(x => x
      .WithTags("ProductEndpoints"));
  }

  public override async Task HandleAsync(
    ProductListRequest request,
    CancellationToken cancellationToken)
  {
    var count = await _repository.CountAsync(cancellationToken);
    var products = await _repository.ListAsync(request.PerPage, request.Page, null, cancellationToken);
    var response = new ProductListResponse(count, request.Page, request.PerPage, BuildRoute)
    {
      Products = products
        .Select(ProductRecord.FromProduct)
        .ToList(),
    };

    await SendAsync(response, cancellation: cancellationToken);
  }
}
