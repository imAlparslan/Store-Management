using InventoryManagement.Domain.StockAggregateRoot;

namespace InventoryManagement.Application.Stocks.Queries.GetStockById;

public sealed record GetStockByIdQuery(Guid StockId) : IQuery<Result<Stock?>>;