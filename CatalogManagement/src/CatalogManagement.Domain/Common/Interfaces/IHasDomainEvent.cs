namespace CatalogManagement.Domain.Common.Interfaces;
public interface IHasDomainEvent
{
    public void AddDomainEvent(IDomainEvent domainEvent);
    public Queue<IDomainEvent> GetDomainEvents();
    public void ClearDomainEvents();
}
