using InventoryManagement.Domain.StockAggregateRoot;

namespace InventoryManagement.Application.Stocks.Queries.GetStockByStoreId;

public sealed record GetStockByStoreIdQuery(Guid StoreId) : IQuery<Result<Stock?>>;
