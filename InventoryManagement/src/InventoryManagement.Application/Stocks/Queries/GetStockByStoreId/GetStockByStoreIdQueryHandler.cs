using InventoryManagement.Application.Common;
using InventoryManagement.Domain.StockAggregateRoot;
using InventoryManagement.Domain.StockAggregateRoot.Errors;

namespace InventoryManagement.Application.Stocks.Queries.GetStockByStoreId;

internal sealed class GetStockByStoreIdQueryHandler(IStockRepository stockRepository) : IQueryHandler<GetStockByStoreIdQuery, Result<Stock?>>
{
    private readonly IStockRepository _stockRepository = stockRepository;

    public async Task<Result<Stock?>> Handle(GetStockByStoreIdQuery request, CancellationToken cancellationToken)
    {
        var result = await _stockRepository.GetStockByStoreIdAsync(request.StoreId, cancellationToken);
        if (result is null)
        {
            return StockErrors.StockNotFound;
        }

        return result;
    }
}