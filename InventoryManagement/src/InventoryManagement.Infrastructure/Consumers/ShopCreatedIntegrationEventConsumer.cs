using MassTransit;
using SharedKernel.IntegrationEvents.Abstract;
using SharedKernel.IntegrationEvents.StoreDefinition;

namespace InventoryManagement.Infrastructure.Consumers;
public class ShopCreatedIntegrationEventConsumer : IEventConsumer<ShopCreatedIntegrationEvent>
{
    public async Task Consume(ConsumeContext<ShopCreatedIntegrationEvent> context)
    {
        Console.WriteLine($"CreatedShopId: {context.Message.ShopId}");

        await Task.CompletedTask;
    }
}

