using SharedKernel.IntegrationEvents.Abstract;
using System;

namespace SharedKernel.IntegrationEvents.StoreDefinition;

public class ShopCreatedIntegrationEvent : IIntegrationEvent
{
    public Guid ShopId { get; set; }

    public ShopCreatedIntegrationEvent(Guid shopId)
    {
        ShopId = shopId;
    }
}
