using Ardalis.HttpClientTestExtensions;
using Net.Advanced.Web;
using Net.Advanced.Web.Endpoints.ProductEndpoints;
using Xunit;

namespace Net.Advanced.FunctionalTests.ApiEndpoints.Product;

[Collection("Sequential")]
public class ListTests : BaseWebFixture
{
  public ListTests(CustomWebApplicationFactory<WebMarker> factory)
    : base(factory)
  {
  }

  [Fact]
  public async Task ReturnsAllProducts()
  {
    // Act.
    var result = await Client.GetAndDeserializeAsync<ProductListResponse>(List.Route);

    // Assert.
    Assert.Equal(2, result.Products.Count);
    Assert.Contains(result.Products, i => i.Name == SeedData.Product1.Name);
  }
}
