namespace InventoryManagement.Contracts.Stocks;
public sealed record StockResponse(Guid Id, Guid StoreId, IReadOnlyList<StockItemResponse> StockItems, IReadOnlyList<Guid> Groups);
