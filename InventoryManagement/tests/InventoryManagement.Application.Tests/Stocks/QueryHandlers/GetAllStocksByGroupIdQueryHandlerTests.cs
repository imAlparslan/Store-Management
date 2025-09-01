using InventoryManagement.Application.Common;
using InventoryManagement.Application.Stocks.Queries.GetAllStocksByGroupId;
using InventoryManagement.Application.Tests.Factories.StockFactories;
using InventoryManagement.Domain.StockAggregateRoot.Errors;

namespace InventoryManagement.Application.Tests.Stocks.QueryHandlers;

public class GetAllStocksByGroupIdQueryHandlerTests
{
    private readonly IStockRepository _stockRepository;
    private readonly GetAllStocksByGroupIdQueryHandler _queryHandler;

    public GetAllStocksByGroupIdQueryHandlerTests()
    {
        _stockRepository = Substitute.For<IStockRepository>();
        _queryHandler = new GetAllStocksByGroupIdQueryHandler(_stockRepository);
    }

    [Fact]
    public async Task Handler_Returns_Stocks_WhenTheyInGivenGroup()
    {
        var groupId = Guid.CreateVersion7();
        var stock1 = StockFactory.CreateValid();
        stock1.TryAddGroup(groupId);
        var stock2 = StockFactory.CreateValid();
        stock2.TryAddGroup(groupId);
        _stockRepository.GetAllStocksByGroupIdAsync(Arg.Any<Guid>(), default).Returns([stock1, stock2]);
        var query = new GetAllStocksByGroupIdQuery(groupId);

        var result = await _queryHandler.Handle(query, default);

        result.IsSuccess.ShouldBeTrue();
        result.Value.ShouldNotBeNull();
        result.Value.Count().ShouldBe(2);
    }

    [Fact]
    public async Task Handler_Returns_EmptyList_WhenNoStockExistsWithGivenGroup()
    {
        _stockRepository.GetAllStocksByGroupIdAsync(Arg.Any<Guid>(), default).Returns([]);
        var query = new GetAllStocksByGroupIdQuery(Guid.CreateVersion7());

        var result = await _queryHandler.Handle(query, default);

        result.IsSuccess.ShouldBeFalse();
        result.Value.ShouldBeNull();
        result.Errors.ShouldNotBeNull();
        result.Errors.ShouldSatisfyAllConditions(
            x => x.Count().ShouldBe(1),
            x => x.First().Code.ShouldBe(StockErrors.StockNotFound.Code),
            x => x.First().Description.ShouldBe(StockErrors.StockNotFound.Description)
        );
    }
}
