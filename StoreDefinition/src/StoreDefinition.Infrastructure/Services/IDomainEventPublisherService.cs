using StoreDefinition.Domain.Common.Interfaces;

namespace StoreDefinition.Infrastructure.Services;
public interface IDomainEventPublisherService
{
    void AddDomainEvent(IDomainEvent domainEvent);
    void AddDomainEvent(IEnumerable<IDomainEvent> domainEvent);
    Task PublishAllAsync(CancellationToken cancellationToken = default);
}
