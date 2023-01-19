using Net.Advanced.Core.CatalogAggregate;
using Net.Advanced.SharedKernel.Interfaces;
using FastEndpoints;

namespace Net.Advanced.Web.Endpoints.CategoryEndpoints;

public class Create : Endpoint<CreateCategoryRequest, CategoryRecord>
{
  private readonly IRepository<Category> _repository;

  public Create(IRepository<Category> repository)
  {
    _repository = repository;
  }

  public override void Configure()
  {
    Post(CreateCategoryRequest.Route);
    AllowAnonymous();
    Options(x => x
      .WithTags("CategoryEndpoints"));
  }
  public override async Task HandleAsync(
    CreateCategoryRequest request,
    CancellationToken cancellationToken)
  {
    if (request.Name is null)
    {
      ThrowError("Name is required");
    }

    var newCategory = new Category(request.Name);
    newCategory.UpdateImage(request.Image);
    if (request.ParentId is not null)
    {
      var parentCategory = await _repository.GetByIdAsync(request.ParentId.Value, cancellationToken);
      if (parentCategory is null)
      {
        ThrowError("Parent was not found");
      }

      newCategory.UpdateParent(parentCategory);
    }
    var createdItem = await _repository.AddAsync(newCategory, cancellationToken);
    var response = CategoryRecord.FromCategory(createdItem);

    await SendAsync(response, cancellation: cancellationToken);
  }
}
