using System.Net;
using Ardalis.HttpClientTestExtensions;
using Net.Advanced.Web;
using Net.Advanced.Web.Endpoints.ProductEndpoints;
using Xunit;

namespace Net.Advanced.FunctionalTests.ApiEndpoints.Product;

[Collection("Sequential")]
public class DeleteTests : BaseWebFixture
{
  public DeleteTests(CustomWebApplicationFactory<WebMarker> factory)
    : base(factory)
  {
  }

  [Fact]
  public async Task DeletesSeededProduct()
  {
    // Arrange.
    string route = DeleteProductRequest.BuildRoute(SeedData.Product2.Id);

    // Act.
    var response = await Client.DeleteAndEnsureNoContentAsync(route);

    // Assert.
    response.EnsureSuccessStatusCode();
    Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
  }

  [Fact]
  public async Task NotFound()
  {
    // Arrange.
    const int productId = 0;
    string route = DeleteProductRequest.BuildRoute(productId);

    // Act.
    var response = await Client.DeleteAndEnsureNotFoundAsync(route);

    // Assert.
    Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
  }
}
