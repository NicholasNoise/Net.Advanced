﻿using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using Net.Advanced.Core.ProjectAggregate;
using Net.Advanced.SharedKernel.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace Net.Advanced.Web.Endpoints.ProjectEndpoints;

public class List : EndpointBaseAsync
    .WithoutRequest
    .WithActionResult<ProjectListResponse>
{
  private readonly IReadRepository<Project> _repository;

  public List(IReadRepository<Project> repository)
  {
    _repository = repository;
  }

  [HttpGet("/Projects")]
  [SwaggerOperation(
      Summary = "Gets a list of all Projects",
      Description = "Gets a list of all Projects",
      OperationId = "Project.List",
      Tags = new[] { "ProjectEndpoints" })
  ]
  public override async Task<ActionResult<ProjectListResponse>> HandleAsync(
    CancellationToken cancellationToken = default(CancellationToken))
  {
    var projects = await _repository.ListAsync(cancellationToken);
    var response = new ProjectListResponse
    {
      Projects = projects
        .Select(project => new ProjectRecord(project.Id, project.Name))
        .ToList(),
    };

    return Ok(response);
  }
}
