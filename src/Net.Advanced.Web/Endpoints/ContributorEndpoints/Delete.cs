﻿using Ardalis.Result;
using FastEndpoints;
using Net.Advanced.Core.Interfaces;

namespace Net.Advanced.Web.Endpoints.ContributorEndpoints;

public class Delete : Endpoint<DeleteContributorRequest>
{
  private readonly IDeleteContributorService _deleteContributorService;

  public Delete(IDeleteContributorService service)
  {
    _deleteContributorService = service;
  }

  public override void Configure()
  {
    Delete(DeleteContributorRequest.Route);
    AllowAnonymous();
    Options(x => x
      .WithTags("ContributorEndpoints"));
  }

  public override async Task HandleAsync(
    DeleteContributorRequest request,
    CancellationToken ct)
  {
    var result = await _deleteContributorService.DeleteContributor(request.ContributorId);

    if (result.Status == ResultStatus.NotFound)
    {
      await SendNotFoundAsync(ct);
      return;
    }

    await SendNoContentAsync(ct);
  }
}
