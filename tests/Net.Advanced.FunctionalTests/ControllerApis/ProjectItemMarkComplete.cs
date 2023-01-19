using System.Text;
using Net.Advanced.Web;
using Newtonsoft.Json;
using Xunit;

namespace Net.Advanced.FunctionalTests.ControllerApis;

[Collection("Sequential")]
public class ProjectItemMarkComplete : IClassFixture<CustomWebApplicationFactory<WebMarker>>
{
  private readonly HttpClient _client;

  public ProjectItemMarkComplete(CustomWebApplicationFactory<WebMarker> factory)
  {
    _client = factory.CreateClient();
  }

  [Fact]
  public async Task MarksIncompleteItemComplete()
  {
    var projectId = 1;
    var itemId = 1;

    var jsonContent = new StringContent(JsonConvert.SerializeObject(null), Encoding.UTF8, "application/json");

    var response = await _client.PatchAsync($"api/projects/{projectId}/complete/{itemId}", jsonContent);
    response.EnsureSuccessStatusCode();

    var stringResponse = await response.Content.ReadAsStringAsync();
    Assert.Equal(string.Empty, stringResponse);
  }
}
