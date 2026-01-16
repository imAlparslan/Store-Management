using InventoryManagement.Application.Common;
using InventoryManagement.Domain.StockAggregateRoot;
using InventoryManagement.Domain.StockAggregateRoot.Errors;

namespace InventoryManagement.Application.Stocks.Queries.GetAllStocksByGroupId;

internal sealed class GetAllStocksByGroupIdQueryHandler(IStockRepository stockRepository) : IQueryHandler<GetAllStocksByGroupIdQuery, Result<IEnumerable<Stock>>>
{
    private readonly IStockRepository _stockRepository = stockRepository;

    public async Task<Result<IEnumerable<Stock>>> Handle(GetAllStocksByGroupIdQuery request, CancellationToken cancellationToken)
    {
        var result = await _stockRepository.GetAllStocksByGroupIdAsync(request.GroupId, cancellationToken);

        if (result is null || !result.Any())
        {
            return StockErrors.StockNotFound;
        }
        return Result<IEnumerable<Stock>>.Success(result);
    }
}