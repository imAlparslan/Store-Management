namespace SharedKernel.Domain.Common.DomainEventAbstraction;
public interface IHasDomainEvent
{
    public void AddDomainEvent(IDomainEvent domainEvent);
    public Queue<IDomainEvent> GetDomainEvents();
    public void ClearDomainEvents();
}

