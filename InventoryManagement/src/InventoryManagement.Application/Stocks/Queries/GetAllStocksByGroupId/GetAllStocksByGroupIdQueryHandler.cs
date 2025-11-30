using InventoryManagement.Application.Common;
using InventoryManagement.Domain.StockAggregateRoot;

namespace InventoryManagement.Application.Stocks.Queries.GetStockByGroupId;
internal sealed class GetAllStocksByGroupIdQueryHandler(IStockRepository stockRepository) : IQueryHandler<GetAllStocksByGroupIdQuery, Result<List<Stock>>>
{
    private readonly IStockRepository _stockRepository = stockRepository;

    public async Task<Result<List<Stock>>> Handle(GetAllStocksByGroupIdQuery request, CancellationToken cancellationToken)
    {
        return await _stockRepository.GetAllStocksByGroupId(request.GroupId, cancellationToken);

    }
}
