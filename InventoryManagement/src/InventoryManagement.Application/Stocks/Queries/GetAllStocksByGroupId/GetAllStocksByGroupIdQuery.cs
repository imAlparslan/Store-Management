using InventoryManagement.Domain.StockAggregateRoot;

namespace InventoryManagement.Application.Stocks.Queries.GetStockByGroupId;

public sealed record GetAllStocksByGroupIdQuery(Guid GroupId) : IQuery<Result<List<Stock>>>;