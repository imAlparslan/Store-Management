using InventoryManagement.Application.Stocks.Commands.AddStockItem;
using InventoryManagement.Application.Stocks.Commands.IncreaseStockCapacity;
using InventoryManagement.Application.Stocks.Queries.GetAllStocksByGroupId;
using InventoryManagement.Application.Stocks.Queries.GetStockById;
using InventoryManagement.Application.Stocks.Queries.GetStockByStoreId;
using InventoryManagement.Contracts;
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

    public static IncreaseStockItemCapacityCommand MapToCommand(this IncreaseStockCapacityRequest request, Guid stockId)
        => new IncreaseStockItemCapacityCommand(stockId, request.ItemId, request.Amount);

    public static GetStockByStoreIdQuery MapToQuery(this GetStocksByStoreIdRequest request)
        => new GetStockByStoreIdQuery(request.StoreId);

    public static GetStockByIdQuery MapToQuery(this GetStockByIdRequest request)
        => new GetStockByIdQuery(request.StockId);
}
