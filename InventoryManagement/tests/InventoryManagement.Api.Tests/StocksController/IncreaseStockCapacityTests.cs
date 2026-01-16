using InventoryManagement.Api.Tests.Factories;

namespace InventoryManagement.Api.Tests.StocksController;

[Collection(nameof(StocksControllerCollectionFixture))]
public class IncreaseStockCapacityTests(InventoryApiFactory apiFactory) : ControllerTestBase(apiFactory)
{

    [Fact]
    public async Task IncreaseStockCapacity_ShouldReturnUpdatedStock_WhenGivenValidInput()
    {
        var stock = StockFactory.CreateValid();
        var stockItem = new StockItem(Guid.NewGuid());
        stock.TryAddItem(stockItem);
        var insertedStock = await InsertStockAsync(stock);
        var request = new IncreaseStockCapacityRequest(stockItem.Id, 50);

        var response = await _client.PutAsJsonAsync($"{StockEndpoints.StockBase}/{stock.Id}/increase-stock-capacity", request);

        response.StatusCode.ShouldBe(HttpStatusCode.OK);
        var data = await response.Content.ReadFromJsonAsync<StockResponse>();
        data.ShouldNotBeNull();
        var updatedStockItem = data.StockItems.First(x => x.Id == stockItem.Id);
        updatedStockItem.Capacity.ShouldBe(50);
    }
    [Fact]
    public async Task IncreaseStockCapacity_ReturnsNotFoundError_WhenStockDoesNotExist()
    {
        var stockId = Guid.NewGuid();
        var stockItemId = Guid.NewGuid();
        var request = new IncreaseStockCapacityRequest(stockItemId, 50);

        var response = await _client.PutAsJsonAsync($"{StockEndpoints.StockBase}/{stockId}/increase-stock-capacity", request);

        response.StatusCode.ShouldBe(HttpStatusCode.NotFound);
        var detail = await response.Content.ReadFromJsonAsync<ProblemDetails>();
        detail.ShouldNotBeNull();
        detail.Title.ShouldBe(StockErrors.StockNotFound.Code);

    }
    [Fact]
    public async Task IncreaseStockCapacity_ReturnsStockItemNotFoundError_WhenStockDoesNotContainStockItem()
    {
        var stock = await InsertStockAsync(StockFactory.CreateValid());
        var stockItemId = Guid.NewGuid();
        var request = new IncreaseStockCapacityRequest(stockItemId, 50);

        var response = await _client.PutAsJsonAsync($"{StockEndpoints.StockBase}/{stock.Id}/increase-stock-capacity", request);

        response.StatusCode.ShouldBe(HttpStatusCode.NotFound);
        var detail = await response.Content.ReadFromJsonAsync<ProblemDetails>();
        detail.ShouldNotBeNull();
        detail.Title.ShouldBe(StockErrors.StockItemNotFound.Code);

    }
    [Fact]
    public async Task IncreaseStockCapacity_ReturnsValidationError_WhenItemIdIsInvalid()
    {
        var request = new IncreaseStockCapacityRequest(Guid.Empty, 50);

        var response = await _client.PutAsJsonAsync($"{StockEndpoints.StockBase}/{Guid.NewGuid()}/increase-stock-capacity", request);

        response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        var detail = await response.Content.ReadFromJsonAsync<ValidationProblemDetails>();
        detail.ShouldNotBeNull();
        detail.Errors.ShouldContainKey(nameof(request.StockItemId));

    }
    [Fact]
    public async Task IncreaseStockCapacity_ReturnsValidationError_WhenStockIdIsInvalid()
    {
        var request = new IncreaseStockCapacityRequest(Guid.NewGuid(), 50);

        var response = await _client.PutAsJsonAsync($"{StockEndpoints.StockBase}/{Guid.Empty}/increase-stock-capacity", request);

        response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        var detail = await response.Content.ReadFromJsonAsync<ValidationProblemDetails>();
        detail.ShouldNotBeNull();
        detail.Errors.ShouldContainKey("StockId");
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-5)]
    public async Task IncreaseStockCapacity_ReturnsValidationError_WhenAmountInvalid(int invalidAmount)
    {
        var request = new IncreaseStockCapacityRequest(Guid.NewGuid(), invalidAmount);

        var response = await _client.PutAsJsonAsync($"{StockEndpoints.StockBase}/{Guid.NewGuid()}/increase-stock-capacity", request);

        response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        var detail = await response.Content.ReadFromJsonAsync<ValidationProblemDetails>();
        detail.ShouldNotBeNull();
        detail.Errors.ShouldContainKey(nameof(request.Amount));
    }
}