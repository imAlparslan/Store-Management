using SharedKernel.IntegrationEvents.Abstract;
namespace SharedKernel.IntegrationEvents.StoreDefinition;

public record ShopCreatedIntegrationEvent(Guid ShopId) : IIntegrationEvent;