using InventoryManagement.Application.Common;
using InventoryManagement.Domain.StockAggregateRoot;
using InventoryManagement.Domain.StockAggregateRoot.Entities;
using InventoryManagement.Domain.StockAggregateRoot.Errors;

namespace InventoryManagement.Application.Stocks.Commands.AddStockItem;
internal sealed class AddStockItemCommandHandler(IStockRepository stockRepository) : ICommandHandler<AddStockItemCommand, Result<Stock>>
{
    private readonly IStockRepository _stockRepository = stockRepository;

    public async Task<Result<Stock>> Handle(AddStockItemCommand request, CancellationToken cancellationToken)
    {
        var stock = await _stockRepository.GetStockByStockId(request.StockId, cancellationToken);

        if (stock is null)
        {
            return StockErrors.StockNotFound;
        }
        var stockItem = new StockItem(request.ItemId, new(request.InitialQuantity), new(request.InitialCapacity));
        var result = stock.TryAddItem(stockItem);
      
        if (!result)
        {
            return StockErrors.StockItemAlreadyExists;
        }
        return await _stockRepository.UpdateStock(stock, cancellationToken);
    }
}
