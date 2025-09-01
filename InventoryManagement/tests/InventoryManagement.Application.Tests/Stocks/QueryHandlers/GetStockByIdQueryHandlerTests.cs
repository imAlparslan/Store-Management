using InventoryManagement.Application.Common;
using InventoryManagement.Application.Stocks.Queries.GetStockById;
using InventoryManagement.Application.Tests.Factories.StockFactories;
using InventoryManagement.Domain.StockAggregateRoot.Errors;
using InventoryManagement.Domain.StockAggregateRoot.ValueObjects;

namespace InventoryManagement.Application.Tests.Stocks.QueryHandlers;

public class GetStockByIdQueryHandlerTests
{
    private readonly IStockRepository _stockRepository;
    private GetStockByIdQueryHandler _queryHandler;
    public GetStockByIdQueryHandlerTests()
    {
        _stockRepository = Substitute.For<IStockRepository>();
        _queryHandler = new(_stockRepository);
    }

    [Fact]
    public async Task Handler_Returns_Stock_When_StockExistsWithGivenStockId()
    {
        var stock = StockFactory.CreateValid();
        _stockRepository.GetStockByStockIdAsync(Arg.Any<StockId>(), default).Returns(stock);
        var query = new GetStockByIdQuery(stock.Id.Value);

        var result = await _queryHandler.Handle(query, default);

        result.IsSuccess.ShouldBeTrue();
        result.Value.ShouldNotBeNull();
        result.Errors.ShouldBeNull();
        result.Value.Id.ShouldBe(stock.Id);
    }

    [Fact]
    public async Task Handler_Returns_NotFoundError_WhenGivenIdNotExists()
    {
        _stockRepository.GetStockByStockIdAsync(Arg.Any<StockId>(), default).ReturnsNullForAnyArgs();
        var query = new GetStockByIdQuery(Guid.CreateVersion7());

        var result = await _queryHandler.Handle(query, default);

        result.IsSuccess.ShouldBeFalse();
        result.Value.ShouldBeNull();
        result.Errors.ShouldHaveSingleItem();
        result.Errors.First().ShouldSatisfyAllConditions
        (
            x => x.Code.ShouldBe(StockErrors.StockNotFound.Code),
            x => x.Description.ShouldBe(StockErrors.StockNotFound.Description)
        );
    }
}
