using SharedKernel.Domain.Common.DomainEventAbstraction;

namespace SharedKernel.Domain.Common.Models;
public abstract class AggregateRoot<TId> : Entity<TId>, IHasDomainEvent where TId : notnull
{
    protected readonly Queue<IDomainEvent> _domainEvents = new();

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
    protected AggregateRoot(TId id) : base(id)
    {

    }

    protected AggregateRoot()
    {

    }
}
