using InventoryManagement.Api.Tests.Factories;

namespace InventoryManagement.Api.Tests.StocksController;

[Collection(nameof(StocksControllerCollectionFixture))]
public class GetStocksByIdTests(InventoryApiFactory apiFactory) : ControllerTestBase(apiFactory)
{
    [Fact]
    public async Task GetStocksByStockId_ReturnsStock_WhenStockHasTheId()
    {
        var stock = await InsertStockAsync(StockFactory.CreateValid());

        var getByIdResponse = await _client.GetAsync($"{StockEndpoints.StockBase}/{stock.Id.Value}");

        getByIdResponse.ShouldNotBeNull();
        getByIdResponse.StatusCode.ShouldBe(HttpStatusCode.OK);
        StockResponse? foundStock = await getByIdResponse.Content.ReadFromJsonAsync<StockResponse>();
        foundStock.ShouldNotBeNull();
        foundStock.Id.ShouldBe(stock.Id.Value);

    }
    [Fact]
    public async Task GetStockById_ReturnsCorrectStock_WhenMultipleStockExists()
    {
        var stock = await InsertStockAsync(StockFactory.CreateValid());
        var stock2 = await InsertStockAsync(StockFactory.CreateValid());
        var stock3 = await InsertStockAsync(StockFactory.CreateValid());

        var getByIdResponse = await _client.GetAsync($"{StockEndpoints.StockBase}/{stock.Id.Value}");

        getByIdResponse.ShouldNotBeNull();
        getByIdResponse.StatusCode.ShouldBe(HttpStatusCode.OK);
        StockResponse? foundStock = await getByIdResponse.Content.ReadFromJsonAsync<StockResponse>();
        foundStock.ShouldNotBeNull();
        foundStock.Id.ShouldBe(stock.Id.Value);
    }

    [Fact]
    public async Task GetStockById_ReturnsNotFound_WhenGivenIdNotExists()
    {
        var getByIdResponse = await _client.GetAsync($"{StockEndpoints.StockBase}/{Guid.NewGuid()}");

        getByIdResponse.ShouldNotBeNull();
        getByIdResponse.StatusCode.ShouldBe(HttpStatusCode.NotFound);
    }
    [Theory]
    [ClassData(typeof(InvalidGuidData))]
    public async Task GetStockById_ReturnsValidationError_WhenGivenIdNotCorrect(string guid)
    {
        var getByIdResponse = await _client.GetAsync($"{StockEndpoints.StockBase}/{guid}");

        getByIdResponse.ShouldNotBeNull();
        getByIdResponse.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        ValidationProblemDetails? error = await getByIdResponse.Content.ReadFromJsonAsync<ValidationProblemDetails>();
        error.ShouldNotBeNull();
        error.Errors.Count.ShouldBe(1);
    }


}
