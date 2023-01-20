using FastEndpoints;
using Net.Advanced.Core.CatalogAggregate;
using Net.Advanced.Core.CatalogAggregate.Specifications;
using Net.Advanced.SharedKernel.Interfaces;

namespace Net.Advanced.Web.Endpoints.CategoryEndpoints;

public class GetById : Endpoint<GetCategoryByIdRequest, CategoryRecord>
{
  private readonly IRepository<Category> _repository;

  public GetById(IRepository<Category> repository)
  {
    _repository = repository;
  }

  public override void Configure()
  {
    Get(GetCategoryByIdRequest.Route);
    AllowAnonymous();
    Options(x => x
      .WithTags("CategoryEndpoints"));
  }

  public override async Task HandleAsync(
    GetCategoryByIdRequest request,
    CancellationToken cancellationToken)
  {
    var spec = new CategoryByIdSpec(request.CategoryId);
    var entity = await _repository.FirstOrDefaultAsync(spec, cancellationToken);
    if (entity == null)
    {
      await SendNotFoundAsync(cancellationToken);
      return;
    }

    var response = CategoryRecord.FromCategory(entity);

    await SendAsync(response, cancellation: cancellationToken);
  }
}
