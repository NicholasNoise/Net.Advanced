using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using Net.Advanced.SharedKernel.Interfaces;
using Net.Advanced.Core.CatalogAggregate;
using Swashbuckle.AspNetCore.Annotations;

namespace Net.Advanced.Web.Endpoints.CategoryEndpoints;

public class Delete : EndpointBaseAsync
  .WithRequest<DeleteCategoryRequest>
  .WithoutResult
{
  private readonly IRepository<Category> _repository;

  public Delete(IRepository<Category> repository)
  {
    _repository = repository;
  }

  [HttpDelete(DeleteCategoryRequest.Route)]
  [SwaggerOperation(
    Summary = "Deletes a Category",
    Description = "Deletes a Category",
    OperationId = "Categories.Delete",
    Tags = new[] { "CategoryEndpoints" })
  ]
  public override async Task<ActionResult> HandleAsync(
    [FromRoute] DeleteCategoryRequest request,
    CancellationToken cancellationToken = default)
  {
    var categoryToDelete = await _repository.GetByIdAsync(request.CategoryId, cancellationToken);
    if (categoryToDelete is null)
    {
      return NotFound();
    }

    await _repository.DeleteAsync(categoryToDelete, cancellationToken);

    return NoContent();
  }
}
