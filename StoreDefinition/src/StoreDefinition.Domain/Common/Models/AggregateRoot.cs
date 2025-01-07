using StoreDefinition.Domain.Common.Interfaces;

namespace StoreDefinition.Domain.Common.Models;
public abstract class AggregateRoot<TId> : Entity<TId>, IHasDomainEvent where TId : notnull
{
    protected readonly Queue<IDomainEvent> _domainEvents = new();

    protected AggregateRoot(TId Id):base(Id)
    {
        
    }
    protected AggregateRoot()
    {
        
    }

    public void AddDomainEvent(IDomainEvent domainEvent)
    {
        _domainEvents.Enqueue(domainEvent);
    }
    public Queue<IDomainEvent> GetDomainEvents()
    {
        return _domainEvents;
    }
    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }
}
