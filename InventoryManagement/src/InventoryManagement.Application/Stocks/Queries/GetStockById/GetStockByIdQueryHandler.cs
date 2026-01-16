using InventoryManagement.Application.Common;
using InventoryManagement.Domain.StockAggregateRoot;
using InventoryManagement.Domain.StockAggregateRoot.Errors;

namespace InventoryManagement.Application.Stocks.Queries.GetStockById;


internal sealed class GetStockByIdQueryHandler(IStockRepository stockRepository) : IQueryHandler<GetStockByIdQuery, Result<Stock?>>
{
    private readonly IStockRepository _stockRepository = stockRepository;

    public async Task<Result<Stock?>> Handle(GetStockByIdQuery request, CancellationToken cancellationToken)
    {
        var stock = await _stockRepository.GetStockByStockIdAsync(request.StockId, cancellationToken);
        if (stock is null)
        {
            return StockErrors.StockNotFound;
        }
        return stock;
    }
}