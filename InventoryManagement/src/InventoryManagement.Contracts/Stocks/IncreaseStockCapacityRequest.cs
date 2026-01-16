namespace InventoryManagement.Contracts.Stocks;
public sealed record IncreaseStockCapacityRequest(Guid StockItemId, int Amount);