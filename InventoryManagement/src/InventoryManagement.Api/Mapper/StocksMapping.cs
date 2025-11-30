using InventoryManagement.Application.Stocks.Commands.AddStockItem;
using InventoryManagement.Application.Stocks.Queries.GetStockByGroupId;
using InventoryManagement.Contracts.Stocks;
using InventoryManagement.Domain.StockAggregateRoot;

namespace InventoryManagement.Api.Mapper;

public static class StocksMapping
{
    public static StockResponse MapToResponse(this Stock stock)
        => new StockResponse(
            stock.Id,
            stock.StoreId,
            stock.StockItems
                .Select(x => new StockItemResponse(x.Id, x.ItemId, x.Quantity.Value, x.Capacity.Value))
                .ToList(),
            stock.GroupIds);

    public static AddStockItemCommand MapToCommand(this AddStockItemRequest request)
        => new AddStockItemCommand(request.StockId, request.ItemId, request.InitialQuantity, request.InitialCapacity);

    public static GetAllStocksByGroupIdQuery MapToQuery(this GetAllStocksByGroupIdRequest request)
            => new GetAllStocksByGroupIdQuery(request.GroupId);

}
