﻿using Net.Advanced.Web;
using Xunit;

namespace Net.Advanced.FunctionalTests;
public abstract class BaseWebFixture : IClassFixture<CustomWebApplicationFactory<WebMarker>>, IDisposable
{
  protected BaseWebFixture(CustomWebApplicationFactory<WebMarker> factory)
  {
    Client = factory.CreateClient();
  }

  protected HttpClient Client { get; }

  public void Dispose()
  {
    Dispose(true);
    GC.SuppressFinalize(this);
  }

  protected virtual void Dispose(bool disposing)
  {
    if (disposing)
    {
      Client.Dispose();
    }
  }
}
