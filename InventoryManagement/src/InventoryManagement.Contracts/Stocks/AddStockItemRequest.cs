namespace InventoryManagement.Contracts.Stocks;
public sealed record AddStockItemRequest(Guid StockId,
                                         Guid ItemId,
                                         int InitialQuantity = 0,
                                         int InitialCapacity = 0);

