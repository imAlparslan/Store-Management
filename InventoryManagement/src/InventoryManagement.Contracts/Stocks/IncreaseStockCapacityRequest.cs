namespace InventoryManagement.Contracts.Stocks;
public sealed record IncreaseStockCapacityRequest(Guid StockId, Guid ItemId, int Amount);
