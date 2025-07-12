namespace InventoryManagement.Contracts.Stocks;
public sealed record StockItemResponse(Guid Id, Guid ItemId, int Quantity, int Capacity);
