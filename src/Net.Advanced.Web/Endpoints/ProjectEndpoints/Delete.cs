using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using Net.Advanced.Core.ProjectAggregate;
using Net.Advanced.SharedKernel.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace Net.Advanced.Web.Endpoints.ProjectEndpoints;

public class Delete : EndpointBaseAsync
    .WithRequest<DeleteProjectRequest>
    .WithoutResult
{
  private readonly IRepository<Project> _repository;

  public Delete(IRepository<Project> repository)
  {
    _repository = repository;
  }

  /// <inheritdoc/>
  [HttpDelete(DeleteProjectRequest.Route)]
  [SwaggerOperation(
      Summary = "Deletes a Project",
      Description = "Deletes a Project",
      OperationId = "Projects.Delete",
      Tags = new[] { "ProjectEndpoints" })
  ]
  public override async Task<ActionResult> HandleAsync(
      [FromRoute] DeleteProjectRequest request,
      CancellationToken cancellationToken = default(CancellationToken))
  {
    var aggregateToDelete = await _repository.GetByIdAsync(request.ProjectId, cancellationToken);
    if (aggregateToDelete == null)
    {
      return NotFound();
    }

    await _repository.DeleteAsync(aggregateToDelete, cancellationToken);

    return NoContent();
  }
}
