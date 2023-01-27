namespace Net.Advanced.SharedKernel;

// This can be modified to EntityBase<TId> to support multiple key types (e.g. Guid)
public abstract class EntityBase
{
  public int Id { get; set; }

  private readonly List<DomainEventBase> _domainEvents = new();

  public IEnumerable<DomainEventBase> GetDomainEvents() => _domainEvents.AsReadOnly();

  public void ClearDomainEvents() => _domainEvents.Clear();

  protected void RegisterDomainEvent(DomainEventBase domainEvent) => _domainEvents.Add(domainEvent);
}
