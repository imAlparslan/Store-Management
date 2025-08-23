using InventoryManagement.Application.Common;
using InventoryManagement.Application.Stocks.Commands.IncreaseStockCapacity;
using InventoryManagement.Application.Tests.Factories.StockFactories;
using InventoryManagement.Application.Tests.Factories.StockItemFactories;
using InventoryManagement.Domain.StockAggregateRoot;
using InventoryManagement.Domain.StockAggregateRoot.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Application.Tests.Stocks.CommandHandlers;
public class IncreaseStockCapacityTests
{
    private readonly IStockRepository _stockRepository;
    private readonly IncreaseStockItemCapacityCommandHandler _handler;

    public IncreaseStockCapacityTests()
    {
        _stockRepository = Substitute.For<IStockRepository>();
        _handler = new IncreaseStockItemCapacityCommandHandler(_stockRepository);
    }

    [Fact]
    public async Task Handler_IncreasesStockCapacity_WhenStockHasTheStockItem()
    {
        var stockItem = StockItemFactory.CreateValid();
        var stock = StockFactory.CreateValid();
        stock.TryAddItem(stockItem);
        var command = new IncreaseStockItemCapacityCommand(stock.Id, stockItem.Id, 100);
        _stockRepository.GetStockByStockId(Arg.Any<StockId>(), default).ReturnsForAnyArgs(stock);
        _stockRepository.UpdateStock(Arg.Any<Stock>(), default).ReturnsForAnyArgs(stock);

        var result = await _handler.Handle(command, default);

        result.IsSuccess.ShouldBeTrue();
        result.Value.ShouldNotBeNull();
        result.Value.StockItems.FirstOrDefault(x => x.Id == command.StockItemId)
            .ShouldNotBeNull();


    }
}
