using Ardalis.HttpClientTestExtensions;
using Net.Advanced.Web;
using Net.Advanced.Web.Endpoints.CategoryEndpoints;
using Xunit;

namespace Net.Advanced.FunctionalTests.ApiEndpoints.Category;

[Collection("Sequential")]
public class ListTests : BaseWebFixture
{
  public ListTests(CustomWebApplicationFactory<WebMarker> factory)
    : base(factory)
  {
  }

  [Fact]
  public async Task ReturnsAllCategories()
  {
    // Act.
    var result = await Client.GetAndDeserializeAsync<CategoryListResponse>(List.Route);

    // Assert.
    Assert.Equal(2, result.Categories.Count);
    Assert.Contains(result.Categories, i => i.Name == SeedData.Category1.Name);
    Assert.Contains(result.Categories, i => i.Name == SeedData.Category2.Name);
  }
}
