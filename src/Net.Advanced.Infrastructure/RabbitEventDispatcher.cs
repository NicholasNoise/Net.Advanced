using Net.Advanced.SharedKernel;
using Net.Advanced.SharedKernel.Interfaces;
using RabbitMQ.Client.Core.DependencyInjection.Services.Interfaces;

namespace Net.Advanced.Infrastructure;

public class RabbitEventDispatcher : IDomainEventDispatcher
{
  private readonly IProducingService _producer;

  public RabbitEventDispatcher(IProducingService producer)
  {
    _producer = producer;
  }

  public async Task DispatchAndClearEvents(IEnumerable<EntityBase> entitiesWithEvents)
  {
    foreach (var entity in entitiesWithEvents)
    {
      var events = entity.GetDomainEvents().ToArray();
      entity.ClearDomainEvents();
      foreach (var domainEvent in events)
      {
        await _producer.SendAsync(
          domainEvent,
          "exchange.name",
          "routing.key");
      }
    }
  }
}
