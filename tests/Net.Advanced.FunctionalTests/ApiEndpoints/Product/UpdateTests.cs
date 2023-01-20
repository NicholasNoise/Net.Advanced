using Ardalis.HttpClientTestExtensions;
using System.Net;
using Net.Advanced.Web;
using Net.Advanced.Web.Endpoints.ProductEndpoints;
using Xunit;
using Newtonsoft.Json;
using System.Text;

namespace Net.Advanced.FunctionalTests.ApiEndpoints.Product;

[Collection("Sequential")]
public class UpdateTests : BaseWebFixture
{
  public UpdateTests(CustomWebApplicationFactory<WebMarker> factory)
    : base(factory)
  {
  }

  [Fact]
  public async Task UpdatesSeededProduct()
  {
    // Arrange.
    const string route = UpdateProductRequest.Route;
    var request = new UpdateProductRequest
    {
      Id = SeedData.Product1.Id,
      Name = "test api product",
      Description = "test description of the product",
      Image = "https://example.com/favicon.ico",
      CategoryId = SeedData.Category1.Id,
      Price = 0.1m,
      Amount = 10,
    };
    var jsonContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

    // Act.
    var response = await Client.PutAndDeserializeAsync<ProductRecord>(route, jsonContent);

    // Assert.
    Assert.Equal(request.Name, response.Name);
    Assert.Equal(request.Image, response.Image);
  }
}
