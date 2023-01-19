using System.Runtime.CompilerServices;
using MediatR;

namespace Net.Advanced.UnitTests;

public class NoOpMediator : IMediator
{
  /// <inheritdoc/>
  public Task Publish(object notification, CancellationToken cancellationToken = default)
  {
    return Task.CompletedTask;
  }

  /// <inheritdoc/>
  public Task Publish<TNotification>(TNotification notification, CancellationToken cancellationToken = default)
    where TNotification : INotification
  {
    return Task.CompletedTask;
  }

  /// <inheritdoc/>
  public Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default)
  {
    return Task.FromResult<TResponse>(default!);
  }

  /// <inheritdoc/>
  public Task<object?> Send(object request, CancellationToken cancellationToken = default)
  {
    return Task.FromResult<object?>(default);
  }

  /// <inheritdoc/>
  public async IAsyncEnumerable<TResponse> CreateStream<TResponse>(
    IStreamRequest<TResponse> request,
    [EnumeratorCancellation] CancellationToken cancellationToken = default)
  {
    await Task.CompletedTask;
    yield break;
  }

  /// <inheritdoc/>
  public async IAsyncEnumerable<object?> CreateStream(
    object request,
    [EnumeratorCancellation] CancellationToken cancellationToken = default)
  {
    await Task.CompletedTask;
    yield break;
  }
}
