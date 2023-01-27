using MediatR;

namespace Net.Advanced.SharedKernel;

public abstract class DomainEventBase : INotification
{
  public virtual string EventName => GetType().Name;

  public DateTime DateOccurred { get; protected set; } = DateTime.UtcNow;
}
