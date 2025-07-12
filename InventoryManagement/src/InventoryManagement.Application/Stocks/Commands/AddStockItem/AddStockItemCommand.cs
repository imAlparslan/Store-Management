using InventoryManagement.Domain.StockAggregateRoot;

namespace InventoryManagement.Application.Stocks.Commands.AddStockItem;
public sealed record AddStockItemCommand(Guid StockId,
                                         Guid ItemId,
                                         int InitialQuantity = 0,
                                         int InitialCapacity = 0)
                                         : ICommand<Result<Stock>>;


