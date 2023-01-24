using FastEndpoints;
using Net.Advanced.Core.CatalogAggregate;
using Net.Advanced.SharedKernel.Interfaces;

namespace Net.Advanced.Web.Endpoints.CategoryEndpoints;

public class Update : Endpoint<UpdateCategoryRequest, CategoryRecord>
{
  private readonly IRepository<Category> _repository;

  public Update(IRepository<Category> repository)
  {
    _repository = repository;
  }

  public override void Configure()
  {
    Put(CreateCategoryRequest.Route);
    AllowAnonymous();
    Options(x => x
      .WithTags("CategoryEndpoints"));
  }

  public override async Task HandleAsync(
    UpdateCategoryRequest request,
    CancellationToken ct)
  {
    if (request.Name == null)
    {
      ThrowError("Name is required");
    }

    var existingCategory = await _repository.GetByIdAsync(request.Id, ct);
    if (existingCategory == null)
    {
      await SendNotFoundAsync(ct);
      return;
    }

    existingCategory.UpdateName(request.Name);
    existingCategory.UpdateImage(request.Image);

    Category? parentCategory = null;
    if (request.ParentId is not null)
    {
      parentCategory = await _repository.GetByIdAsync(request.ParentId.Value, ct);
      if (parentCategory is null)
      {
        ThrowError("Parent was not found");
      }
    }

    existingCategory.UpdateParent(parentCategory);

    await _repository.UpdateAsync(existingCategory, ct);

    var response = CategoryRecord.FromCategory(existingCategory);

    await SendAsync(response, cancellation: ct);
  }
}
