using System.Text;
using Ardalis.HttpClientTestExtensions;
using Net.Advanced.Web;
using Net.Advanced.Web.Endpoints.CategoryEndpoints;
using Newtonsoft.Json;
using Xunit;

namespace Net.Advanced.FunctionalTests.ApiEndpoints.Category;

[Collection("Sequential")]
public class CreateTests : BaseWebFixture
{
  public CreateTests(CustomWebApplicationFactory<WebMarker> factory)
    : base(factory)
  {
  }

  [Fact]
  public async Task CreatesNewCategory()
  {
    // Arrange.
    const string route = CreateCategoryRequest.Route;
    var request = new CreateCategoryRequest
    {
      Name = "test api category",
      Image = "https://example.com/favicon.ico",
      ParentId = SeedData.Category1.Id,
    };
    var jsonContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

    // Act.
    var response = await Client.PostAndDeserializeAsync<CategoryRecord>(route, jsonContent);

    // Assert.
    Assert.Equal(request.Name, response.Name);
  }
}
