using InventoryManagement.Domain.StockAggregateRoot;

namespace InventoryManagement.Application.Stocks.Commands.IncreaseStockCapacity;
public sealed record IncreaseStockItemCapacityCommand(Guid StockId, Guid StockItemId, int Amount) 
    : ICommand<Result<Stock>>;
