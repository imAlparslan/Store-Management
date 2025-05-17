using InventoryManagement.Application.Common;
using MassTransit;
using Microsoft.Extensions.Logging;
using SharedKernel.IntegrationEvents.Abstract;
using SharedKernel.IntegrationEvents.StoreDefinition;

namespace InventoryManagement.Infrastructure.Consumers;
public class ShopDeletedIntegrationEventConsumer(ILogger<ShopDeletedIntegrationEventConsumer> logger,
                                                 IStockRepository stockRepository) : IEventConsumer<ShopDeletedIntegrationEvent>
{
    private readonly IStockRepository _stockRepository = stockRepository;
    private readonly ILogger<ShopDeletedIntegrationEventConsumer> _logger = logger;
    public async Task Consume(ConsumeContext<ShopDeletedIntegrationEvent> context)
    {
        var stock = await _stockRepository.GetStockByStoreId(context.Message.ShopId);
        if (stock is null)
        {
            _logger.LogInformation($"stock not found with given store Id {context.Message.ShopId}");
            return;
        }
        await _stockRepository.DeleteStockAsync(stock);

        _logger.LogInformation($"Stock deleted {stock.Id}");
    }
}

