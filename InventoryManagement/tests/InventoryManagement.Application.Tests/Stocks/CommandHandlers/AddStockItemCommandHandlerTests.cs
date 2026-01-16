using InventoryManagement.Application.Common;
using InventoryManagement.Application.Stocks.Commands.AddStockItem;
using InventoryManagement.Application.Tests.Factories.StockFactories;
using InventoryManagement.Application.Tests.Factories.StockItemFactories;
using InventoryManagement.Domain.StockAggregateRoot;
using InventoryManagement.Domain.StockAggregateRoot.Errors;
using InventoryManagement.Domain.StockAggregateRoot.ValueObjects;

namespace InventoryManagement.Application.Tests.Stocks.CommandHandlers;
public class AddStockItemCommandHandlerTests
{
    private readonly IStockRepository _stockRepository;
    private readonly AddStockItemCommandHandler _handler;

    public AddStockItemCommandHandlerTests()
    {
        _stockRepository = Substitute.For<IStockRepository>();
        _handler = new AddStockItemCommandHandler(_stockRepository);
    }

    [Fact]
    public async Task Handler_Returns_Stock_WhenStockItem_Added()
    {
        var stock = StockFactory.CreateValid();
        var stockItem = StockItemFactory.CreateValid();
        var command = new AddStockItemCommand(stock.Id, stockItem.Id, 10, 10);
        _stockRepository.GetStockByStockIdAsync(Arg.Any<StockId>()).ReturnsForAnyArgs(stock);
        _stockRepository.UpdateStockAsync(Arg.Any<Stock>(), default).ReturnsForAnyArgs(stock);

        var result = await _handler.Handle(command, default);

        result.IsSuccess.ShouldBeTrue();
        result.Value.ShouldNotBeNull();
        result.Value.StockItems.ShouldContain(x => x.ItemId == command.ItemId);
    }

    [Fact]
    public async Task Handler_Returns_NotFoundError_WhenStockNotFound()
    {
        var command = new AddStockItemCommand(Guid.CreateVersion7(), Guid.CreateVersion7());
        _stockRepository.GetStockByStockIdAsync(Arg.Any<StockId>()).ReturnsNullForAnyArgs();

        var result = await _handler.Handle(command, default);

        result.IsSuccess.ShouldBeFalse();
        result.Value.ShouldBeNull();
        result.Errors.ShouldNotBeNull();
        result.Errors.ShouldContain(StockErrors.StockNotFound);

    }

    [Fact]
    public async Task Handler_Returns_StockItemAlreadyExistsError_WhenStockHasTheStockItem()
    {
        var stockItem = StockItemFactory.CreateValid();
        var stock = StockFactory.CreateValid();
        stock.TryAddItem(stockItem);
        var command = new AddStockItemCommand(stock.Id, stockItem.ItemId, 10, 10);
        _stockRepository.GetStockByStockIdAsync(Arg.Any<StockId>()).ReturnsForAnyArgs(stock);

        var result = await _handler.Handle(command, default);

        result.IsSuccess!.ShouldBeFalse();
        result.Value.ShouldBeNull();
        result.Errors.ShouldNotBeNull();
        result.Errors.ShouldContain(StockErrors.StockItemAlreadyExists);

    }
}