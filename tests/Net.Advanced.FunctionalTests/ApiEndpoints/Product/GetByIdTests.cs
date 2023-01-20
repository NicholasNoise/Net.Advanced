using Ardalis.HttpClientTestExtensions;
using Net.Advanced.Web;
using Net.Advanced.Web.Endpoints.ProductEndpoints;
using Xunit;

namespace Net.Advanced.FunctionalTests.ApiEndpoints.Product;

[Collection("Sequential")]
public class GetByIdTests : BaseWebFixture
{
  public GetByIdTests(CustomWebApplicationFactory<WebMarker> factory)
    : base(factory)
  {
  }

  [Fact]
  public async Task ReturnsProduct()
  {
    // Arrange.
    var productId = SeedData.Product1.Id;
    var route = GetProductByIdRequest.BuildRoute(productId);

    // Act.
    var result = await Client.GetAndDeserializeAsync<ProductRecord>(route);

    // Assert.
    Assert.Equal(productId, result.Id);
    Assert.Equal(SeedData.Product1.Name, result.Name);
  }

  [Fact]
  public async Task ReturnsNotFoundGivenId0()
  {
    // Arrange.
    const int productId = 0;
    var route = GetProductByIdRequest.BuildRoute(productId);

    // Assert.
    _ = await Client.GetAndEnsureNotFoundAsync(route);
  }
}
