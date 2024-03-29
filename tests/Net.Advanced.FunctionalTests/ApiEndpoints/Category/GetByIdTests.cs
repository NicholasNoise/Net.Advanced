﻿using System.Net;
using Ardalis.HttpClientTestExtensions;
using Net.Advanced.Web;
using Net.Advanced.Web.Endpoints.CategoryEndpoints;
using Xunit;

namespace Net.Advanced.FunctionalTests.ApiEndpoints.Category;

[Collection("Sequential")]
public class GetByIdTests : BaseWebFixture
{
  public GetByIdTests(CustomWebApplicationFactory<WebMarker> factory)
    : base(factory)
  {
  }

  [Fact]
  public async Task ReturnsAllCategories()
  {
    // Arrange.
    const int categoryId = 1;
    var route = GetCategoryByIdRequest.BuildRoute(categoryId);

    // Act.
    var result = await Client.GetAndDeserializeAsync<CategoryRecord>(route);

    // Assert.
    Assert.Equal(categoryId, result.Id);
    Assert.Equal(SeedData.Category1.Name, result.Name);
  }

  [Fact]
  public async Task ReturnsNotFoundGivenId0()
  {
    // Arrange.
    const int categoryId = 0;
    var route = GetCategoryByIdRequest.BuildRoute(categoryId);

    // Assert.
    var response = await Client.GetAsync(route);
    Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
  }
}
