using System.Text;
using Ardalis.HttpClientTestExtensions;
using Net.Advanced.Web;
using Net.Advanced.Web.Endpoints.CategoryEndpoints;
using Newtonsoft.Json;
using Xunit;

namespace Net.Advanced.FunctionalTests.ApiEndpoints.Category;

[Collection("Sequential")]
public class UpdateTests : BaseWebFixture
{
  public UpdateTests(CustomWebApplicationFactory<WebMarker> factory)
    : base(factory)
  {
  }

  [Fact]
  public async Task UpdatesSeededCategory()
  {
    // Arrange.
    const string route = UpdateCategoryRequest.Route;
    var request = new UpdateCategoryRequest
    {
      Id = SeedData.Category2.Id,
      Name = "Changed api name",
      Image = "https://example.com/favicon.ico",
      ParentId = SeedData.Category1.Id,
    };
    var jsonContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

    // Act.
    var response = await Client.PutAndDeserializeAsync<CategoryRecord>(route, jsonContent);

    // Assert.
    Assert.Equal(request.Name, response.Name);
    Assert.Equal(request.Image, response.Image);
  }
}
