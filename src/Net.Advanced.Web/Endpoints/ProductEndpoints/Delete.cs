using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using Net.Advanced.Core.CatalogAggregate;
using Net.Advanced.SharedKernel.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace Net.Advanced.Web.Endpoints.ProductEndpoints;

public class Delete : EndpointBaseAsync
  .WithRequest<DeleteProductRequest>
  .WithoutResult
{
  private readonly IRepository<Product> _repository;

  public Delete(IRepository<Product> repository)
  {
    _repository = repository;
  }

  /// <inheritdoc/>
  [HttpDelete(DeleteProductRequest.Route)]
  [SwaggerOperation(
    Summary = "Deletes a Product",
    Description = "Deletes a Product",
    OperationId = "Products.Delete",
    Tags = new[] { "ProductEndpoints" })
  ]
  public override async Task<ActionResult> HandleAsync(
    [FromRoute] DeleteProductRequest request,
    CancellationToken cancellationToken = default)
  {
    var productToDelete = await _repository.GetByIdAsync(request.ProductId, cancellationToken);
    if (productToDelete is null)
    {
      return NotFound();
    }

    await _repository.DeleteAsync(productToDelete, cancellationToken);

    return NoContent();
  }
}
