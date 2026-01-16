using InventoryManagement.Application.Common;
using InventoryManagement.Domain.StockAggregateRoot;
using InventoryManagement.Domain.StockAggregateRoot.Errors;

namespace InventoryManagement.Application.Stocks.Commands.IncreaseStockCapacity;
internal sealed class IncreaseStockItemCapacityCommandHandler(IStockRepository stockRepository)
    : ICommandHandler<IncreaseStockItemCapacityCommand, Result<Stock>>
{
    private readonly IStockRepository _stockRepository = stockRepository;

    public async Task<Result<Stock>> Handle(IncreaseStockItemCapacityCommand request, CancellationToken cancellationToken)
    {
        var stock = await _stockRepository.GetStockByStockIdAsync(request.StockId, cancellationToken);

        if (stock is null)
        {
            return StockErrors.StockNotFound;
        }

        if (!stock.HasStockItem(request.StockItemId))
        {
            return StockErrors.StockItemNotFound;
        }

        stock.IncreaseStockItemCapacity(request.StockItemId, request.Amount);

        return await _stockRepository.UpdateStockAsync(stock, cancellationToken);
    }
}