using System.Text;
using Ardalis.HttpClientTestExtensions;
using Net.Advanced.Web;
using Net.Advanced.Web.Endpoints.ProductEndpoints;
using Newtonsoft.Json;
using Xunit;

namespace Net.Advanced.FunctionalTests.ApiEndpoints.Product;

[Collection("Sequential")]
public class CreateTests : BaseWebFixture
{
  public CreateTests(CustomWebApplicationFactory<WebMarker> factory)
    : base(factory)
  {
  }

  [Fact]
  public async Task CreatesNewProduct()
  {
    // Arrange.
    const string route = CreateProductRequest.Route;
    var request = new CreateProductRequest
    {
      Name = "test api product",
      Description = "test description of the product",
      Image = "https://example.com/favicon.ico",
      CategoryId = SeedData.Category2.Id,
      Price = 0.1m,
      Amount = 10,
    };
    var jsonContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

    // Act.
    var response = await Client.PostAndDeserializeAsync<ProductRecord>(route, jsonContent);

    // Assert.
    Assert.Equal(request.Name, response.Name);
  }
}
