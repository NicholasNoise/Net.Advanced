using FastEndpoints;
using Net.Advanced.Core.CatalogAggregate;
using Net.Advanced.SharedKernel.Interfaces;

namespace Net.Advanced.Web.Endpoints.CategoryEndpoints;

public class List : EndpointWithoutRequest<CategoryListResponse>
{
  private readonly IRepository<Category> _repository;

  public const string Route = "/Categories";

  public List(IRepository<Category> repository)
  {
    _repository = repository;
  }

  /// <inheritdoc/>
  public override void Configure()
  {
    Get(Route);
    AllowAnonymous();
    Options(x => x
      .WithTags("CategoryEndpoints"));
  }

  /// <inheritdoc/>
  public override async Task HandleAsync(CancellationToken cancellationToken)
  {
    var categories = await _repository.ListAsync(cancellationToken);
    var response = new CategoryListResponse
    {
      Categories = categories
        .Select(CategoryRecord.FromCategory)
        .ToList(),
    };

    await SendAsync(response, cancellation: cancellationToken);
  }
}
