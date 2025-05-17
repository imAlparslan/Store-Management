using SharedKernel.IntegrationEvents.Abstract;

namespace SharedKernel.IntegrationEvents.StoreDefinition;
public record ShopDeletedIntegrationEvent(Guid ShopId) : IIntegrationEvent;

