using FastEndpoints;
using Net.Advanced.Core.CatalogAggregate;
using Net.Advanced.SharedKernel.Interfaces;

namespace Net.Advanced.Web.Endpoints.ProductEndpoints;

public class Update : Endpoint<UpdateProductRequest, ProductRecord>
{
  private readonly IRepository<Product> _repository;

  private readonly IRepository<Category> _categoryRepository;

  public Update(IRepository<Product> repository, IRepository<Category> categoryRepository)
  {
    _repository = repository;
    _categoryRepository = categoryRepository;
  }

  public override void Configure()
  {
    Put(CreateProductRequest.Route);
    AllowAnonymous();
    Options(x => x
      .WithTags("ProductEndpoints"));
  }

  public override async Task HandleAsync(
    UpdateProductRequest request,
    CancellationToken ct)
  {
    if (request.Name == null)
    {
      ThrowError("Name is required");
    }

    var existingProduct = await _repository.GetByIdAsync(request.Id, ct);
    if (existingProduct == null)
    {
      await SendNotFoundAsync(ct);
      return;
    }

    var category = await _categoryRepository.GetByIdAsync(request.CategoryId, ct);
    if (category is null)
    {
      ThrowError("Category was not found");
    }

    existingProduct.UpdateName(request.Name);
    existingProduct.Description = request.Description;
    existingProduct.Image = request.Image;
    existingProduct.Price = request.Price;
    existingProduct.Amount = request.Amount;

    await _repository.UpdateAsync(existingProduct, ct);

    var response = ProductRecord.FromProduct(existingProduct);

    await SendAsync(response, cancellation: ct);
  }
}
