using InventoryManagement.Application.Common;
using InventoryManagement.Domain.Common;
using InventoryManagement.Domain.StockAggregateRoot;
using InventoryManagement.Domain.StockAggregateRoot.Entities;
using InventoryManagement.Infrastructure.ProtoHelpers;
using MassTransit;
using Microsoft.Extensions.Logging;
using SharedKernel.IntegrationEvents.Abstract;
using SharedKernel.IntegrationEvents.StoreDefinition;
using StoreDefinitionProtos;

namespace InventoryManagement.Infrastructure.Consumers;
public class ShopCreatedIntegrationEventConsumer(IStockRepository stockRepository,
                                                 StoreDefinitionGrpc.StoreDefinitionGrpcClient storeDefinitionGrpcClient,
                                                 ILogger<ShopCreatedIntegrationEventConsumer> logger,
                                                 IItemRepository itemRepository) : IEventConsumer<ShopCreatedIntegrationEvent>
{
    private readonly IStockRepository _stockRepository = stockRepository;
    private readonly IItemRepository _itemRepository = itemRepository;
    private readonly StoreDefinitionGrpc.StoreDefinitionGrpcClient _storeDefinitionGrpcClient = storeDefinitionGrpcClient;
    private readonly ILogger<ShopCreatedIntegrationEventConsumer> _logger = logger;
    public async Task Consume(ConsumeContext<ShopCreatedIntegrationEvent> context)
    {
        var storeId = context.Message.ShopId;
        var request = new GetStoreGroupsByStoreIdRequest()
        {
            ShopId = storeId.ToProto()
        };
        var replay = await _storeDefinitionGrpcClient.GetStoreGroupsByStoreIdAsync(request);
        var groups = replay.Ids.Select(x => x.ToGuid()).ToList();

        var defaultItems = await _itemRepository.GetAllDefaultStockItems();

        var stockItems = defaultItems
            .Select(x => new StockItem(x.Id))
            .ToList();

        Stock stock = new Stock(new StoreId(storeId), groups, stockItems);

        await _stockRepository.InsertShopAsync(stock);
        _logger.LogInformation($"Stock Inserted - Stock Id: {stock.Id.Value}");
    }
}

