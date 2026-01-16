using InventoryManagement.Domain.StockAggregateRoot.ValueObjects;
using InventoryManagement.Infrastructure.Persistence;
using Microsoft.Extensions.DependencyInjection;

namespace InventoryManagement.Api.Tests.Common;

public abstract class ControllerTestBase : IAsyncLifetime
{
    protected readonly HttpClient _client;
    protected Func<Task> _resetDatabase;
    protected const string StockBaseAddress = "http://localhost/api/stocks";
    protected InventoryApiFactory _apiFactory;

    protected ControllerTestBase(InventoryApiFactory apiFactory)
    {
        _apiFactory = apiFactory;
        _client = apiFactory.client;
        _resetDatabase = apiFactory.ResetDb;

    }
    public async Task DisposeAsync() => await Task.CompletedTask;

    public async Task InitializeAsync() => await _resetDatabase();

    public async Task<Stock> InsertStockAsync(Stock stock)
    {
        using var scope = _apiFactory.Services.CreateScope();

        var dbContext = scope.ServiceProvider.GetRequiredService<InventoryDbContext>();
        await dbContext.Stocks.AddAsync(stock);
        await dbContext.SaveChangesAsync();
        return stock;
    }

    public async Task<Stock> AddStockItemToStock(StockId stockId, StockItem stockItem)
    {
        using var scope = _apiFactory.Services.CreateScope();

        var dbContext = scope.ServiceProvider.GetRequiredService<InventoryDbContext>();

        var stock = dbContext.Stocks.First(x => x.Id == stockId.Value);

        stock.TryAddItem(stockItem);

        await dbContext.SaveChangesAsync();

        return stock;
    }
}