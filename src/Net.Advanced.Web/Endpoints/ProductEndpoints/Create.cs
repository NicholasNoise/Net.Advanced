﻿using FastEndpoints;
using Net.Advanced.Core.CatalogAggregate;
using Net.Advanced.SharedKernel.Interfaces;

namespace Net.Advanced.Web.Endpoints.ProductEndpoints;

public class Create : Endpoint<CreateProductRequest, ProductRecord>
{
  private readonly IRepository<Product> _repository;

  private readonly IRepository<Category> _categoryRepository;

  public Create(IRepository<Product> repository, IRepository<Category> categoryRepository)
  {
    _repository = repository;
    _categoryRepository = categoryRepository;
  }

  public override void Configure()
  {
    Post(CreateProductRequest.Route);
    AllowAnonymous();
    Options(x => x
      .WithTags("ProductEndpoints"));
  }

  public override async Task HandleAsync(
    CreateProductRequest request,
    CancellationToken ct)
  {
    if (request.Name is null)
    {
      ThrowError("Name is required");
    }

    var category = await _categoryRepository.GetByIdAsync(request.CategoryId, ct);
    if (category == null)
    {
      ThrowError("Category was not found");
    }

    var newProduct = new Product(request.Name, request.Price, request.Amount)
    {
      Description = request.Description,
      Image = request.Image,
      Category = category,
    };

    var createdItem = await _repository.AddAsync(newProduct, ct);
    var response = ProductRecord.FromProduct(createdItem);

    await SendAsync(response, cancellation: ct);
  }
}
