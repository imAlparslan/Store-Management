using InventoryManagement.Application.Common;
using InventoryManagement.Domain.StockAggregateRoot;
using InventoryManagement.Infrastructure.ProtoHelpers;
using MassTransit;
using SharedKernel.IntegrationEvents.Abstract;
using SharedKernel.IntegrationEvents.StoreDefinition;
using StoreDefinitionProtos;

namespace InventoryManagement.Infrastructure.Consumers;
public class ShopCreatedIntegrationEventConsumer(IStockRepository stockRepository, StoreDefinitionGrpc.StoreDefinitionGrpcClient storeDefinitionGrpcClient) : IEventConsumer<ShopCreatedIntegrationEvent>
{
    private readonly IStockRepository _stockRepository = stockRepository;
    private readonly StoreDefinitionGrpc.StoreDefinitionGrpcClient _storeDefinitionGrpcClient = storeDefinitionGrpcClient;
    public async Task Consume(ConsumeContext<ShopCreatedIntegrationEvent> context)
    {
        var storeId = context.Message.ShopId;
        var request = new GetStoreGroupsByStoreIdRequest()
        {
            ShopId = storeId.ToProto()
        };
        var replay = await _storeDefinitionGrpcClient.GetStoreGroupsByStoreIdAsync(request);
        var groups = replay.Ids.Select(x => x.ToGuid()).ToList();
        Stock stock = new Stock(storeId, groups);
        Console.WriteLine($"Stock Created - Stock Id: {stock.Id.Value}");
        await Task.CompletedTask;
    }
}

