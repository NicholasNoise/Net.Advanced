﻿using FastEndpoints;
using Net.Advanced.Core.CatalogAggregate;
using Net.Advanced.SharedKernel.Interfaces;

namespace Net.Advanced.Web.Endpoints.ProductEndpoints;

public class GetById : Endpoint<GetProductByIdRequest, ProductRecord>
{
  private readonly IRepository<Product> _repository;

  public GetById(IRepository<Product> repository)
  {
    _repository = repository;
  }

  public override void Configure()
  {
    Get(GetProductByIdRequest.Route);
    AllowAnonymous();
    Options(x => x
      .WithTags("ProductEndpoints"));
  }

  public override async Task HandleAsync(
    GetProductByIdRequest request,
    CancellationToken ct)
  {
    var entity = await _repository.GetByIdAsync(request.ProductId, ct);
    if (entity == null)
    {
      await SendNotFoundAsync(ct);
      return;
    }

    var response = ProductRecord.FromProduct(entity);

    await SendAsync(response, cancellation: ct);
  }
}
