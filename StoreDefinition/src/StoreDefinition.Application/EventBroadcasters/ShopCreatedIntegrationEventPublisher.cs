using SharedKernel.IntegrationEvents.StoreDefinition;
using StoreDefinition.Application.Services;
using StoreDefinition.Domain.ShopAggregateRoot.Events;

namespace StoreDefinition.Application.EventBroadcasters;
public class ShopCreatedIntegrationEventPublisher(IEventPublisher publisher)
    : IDomainEventHandler<ShopCreatedDomainEvent>
{
    private readonly IEventPublisher _publisher = publisher;
    public async Task Handle(ShopCreatedDomainEvent notification, CancellationToken cancellationToken)
    {
        var shopCreatedIntegrationEvent = new ShopCreatedIntegrationEvent(notification.Shop.Id);

        await _publisher.PublishAsync(shopCreatedIntegrationEvent);
    }
}

