using InventoryManagement.Application.Common;
using InventoryManagement.Domain.ItemAggregateRoot;
using InventoryManagement.Domain.ItemAggregateRoot.ValueObjects;
using MassTransit;
using SharedKernel.IntegrationEvents.Abstract;
using SharedKernel.IntegrationEvents.CatalogManagement;

namespace InventoryManagement.Infrastructure.Consumers;
public class ProductCreatedIntegrationEventConsumer(IItemRepository itemRepository) : IEventConsumer<ProductCreatedIntegrationEvent>
{
    private readonly IItemRepository _itemRepository = itemRepository;

    public async Task Consume(ConsumeContext<ProductCreatedIntegrationEvent> context)
    {
        var productDefinition = new ProductDefinition(context.Message.Name, context.Message.Code, context.Message.Definition);

        var item = new Item(new(context.Message.ProductId), productDefinition, context.Message.IsDefault);

        await _itemRepository.InsertItemAsync(item);
    }
}
