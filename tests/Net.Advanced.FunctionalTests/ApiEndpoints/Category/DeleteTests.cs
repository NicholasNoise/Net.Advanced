using System.Net;
using Ardalis.HttpClientTestExtensions;
using Net.Advanced.Web;
using Net.Advanced.Web.Endpoints.CategoryEndpoints;
using Xunit;

namespace Net.Advanced.FunctionalTests.ApiEndpoints.Category;

[Collection("Sequential")]
public class DeleteTests : BaseWebFixture
{
  public DeleteTests(CustomWebApplicationFactory<WebMarker> factory)
    : base(factory)
  {
  }

  [Fact]
  public async Task DeletesSeededCategory()
  {
    // Arrange.
    string route = DeleteCategoryRequest.BuildRoute(SeedData.Category2.Id);

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
    const int categoryId = 0;
    string route = DeleteCategoryRequest.BuildRoute(categoryId);

    // Act.
    var response = await Client.DeleteAndEnsureNotFoundAsync(route);

    // Assert.
    Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
  }
}
