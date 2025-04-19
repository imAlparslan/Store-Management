namespace CatalogManagement.Infrastructure.Services;

public interface IDomainEventPublisherService
{
    void AddDomainEvent(IDomainEvent domainEvent);
    void AddDomainEvent(IEnumerable<IDomainEvent> domainEvent);
    Task PublishAllAsync(CancellationToken cancellationToken = default);
}