using InventoryManagement.Domain.StockAggregateRoot;

namespace InventoryManagement.Application.Stocks.Queries.GetAllStocksByGroupId;

public sealed record GetAllStocksByGroupIdQuery(Guid GroupId) : IQuery<Result<IEnumerable<Stock>>>;
