using FastEndpoints;
using Net.Advanced.Core.ContributorAggregate;
using Net.Advanced.SharedKernel.Interfaces;

namespace Net.Advanced.Web.Endpoints.ContributorEndpoints;

public class Update : Endpoint<UpdateContributorRequest, UpdateContributorResponse>
{
  private readonly IRepository<Contributor> _repository;

  public Update(IRepository<Contributor> repository)
  {
    _repository = repository;
  }

  public override void Configure()
  {
    Put(CreateContributorRequest.Route);
    AllowAnonymous();
    Options(x => x
      .WithTags("ContributorEndpoints"));
  }

  public override async Task HandleAsync(
    UpdateContributorRequest request,
    CancellationToken ct)
  {
    if (request.Name == null)
    {
      ThrowError("Name is required");
    }

    var existingContributor = await _repository.GetByIdAsync(request.Id, ct);
    if (existingContributor == null)
    {
      await SendNotFoundAsync(ct);
      return;
    }

    existingContributor.UpdateName(request.Name);

    await _repository.UpdateAsync(existingContributor, ct);

    var response = new UpdateContributorResponse(
        contributor: new ContributorRecord(existingContributor.Id, existingContributor.Name));

    await SendAsync(response, cancellation: ct);
  }
}
