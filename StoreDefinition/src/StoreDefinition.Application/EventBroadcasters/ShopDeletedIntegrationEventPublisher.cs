using SharedKernel.IntegrationEvents.StoreDefinition;
using StoreDefinition.Application.Services;
using StoreDefinition.Domain.ShopAggregateRoot.Events;

namespace StoreDefinition.Application.EventBroadcasters;
public class ShopDeletedIntegrationEventPublisher(IEventPublisher eventPublisher) : IDomainEventHandler<ShopDeletedDomainEvent>
{
    private readonly IEventPublisher _eventPublisher = eventPublisher;

    public async Task Handle(ShopDeletedDomainEvent notification, CancellationToken cancellationToken)
    {
        var shopDeletedIntegrationEvent = new ShopDeletedIntegrationEvent(notification.ShopId);

        await _eventPublisher.PublishAsync(shopDeletedIntegrationEvent);
    }
}
