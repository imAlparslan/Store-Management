namespace InventoryManagement.Contracts.Stocks;
public sealed record IncreaseStockCapacityRequest(Guid ItemId, int Amount);
