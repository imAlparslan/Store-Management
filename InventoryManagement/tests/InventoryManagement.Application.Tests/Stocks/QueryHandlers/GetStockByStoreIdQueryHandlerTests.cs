using InventoryManagement.Application.Common;
using InventoryManagement.Application.Stocks.Queries.GetStockByStoreId;
using InventoryManagement.Application.Tests.Factories.StockFactories;
using InventoryManagement.Domain.Common;

namespace InventoryManagement.Application.Tests.Stocks.QueryHandlers;

public class GetStockByStoreIdQueryHandlerTests
{
    private readonly IStockRepository _stockRepository;
    private readonly GetStockByStoreIdQueryHandler _handler;

    public GetStockByStoreIdQueryHandlerTests()
    {
        _stockRepository = Substitute.For<IStockRepository>();
        _handler = new(_stockRepository);
    }

    [Fact]
    public async Task Handler_Returns_Stock_WhenStockExistsWithGivenStoreId()
    {
        var stock = StockFactory.CreateValid();
        _stockRepository.GetStockByStoreIdAsync(Arg.Any<StoreId>(), default).Returns(stock);
        var query = new GetStockByStoreIdQuery(stock.StoreId);

        var result = await _handler.Handle(query, default);

        result.IsSuccess.ShouldBeTrue();
        result.Value.ShouldNotBeNull();
        result.Errors.ShouldBeNull();
        result.Value.StoreId.ShouldBe(stock.StoreId);

    }
}
