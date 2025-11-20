namespace InventoryManagement.Api.Tests.StocksController;

[Collection(nameof(StocksControllerCollectionFixture))]
public class AddStockItemTests(InventoryApiFactory apiFactory) : ControllerTestBase(apiFactory)
{

    [Fact]
    public async Task AddStockItem_ReturnsStock_WhenGivenStockNotHaveTheStockItem()
    {
        var stockItemId = Guid.CreateVersion7();
        var stock = await InsertStockAsync(new Stock(Guid.CreateVersion7(), new(), new()));
        var request = new AddStockItemRequest(stock.Id, stockItemId, 10, 10);

        var response = await _client.PostAsJsonAsync(ApiEndpoints.StockEndpoints.AddItem, request);

        response.ShouldNotBeNull();
        response.StatusCode.ShouldBe(HttpStatusCode.OK);
        var data = await response.Content.ReadFromJsonAsync<StockResponse>();
        data.ShouldNotBeNull();
        data.StockItems.Select(x => x.ItemId).ShouldContain(stockItemId);
    }

    [Fact]
    public async Task AddStockItem_ReturnsStockNotFound_WhenStockNotExists()
    {
        var stockId = Guid.CreateVersion7();
        var stockItemId = Guid.CreateVersion7();
        var request = new AddStockItemRequest(stockId, stockItemId, new(), new());

        var response = await _client.PostAsJsonAsync(ApiEndpoints.StockEndpoints.AddItem, request);

        response.ShouldNotBeNull();
        response.StatusCode.ShouldBe(HttpStatusCode.NotFound);
        var detail = await response.Content.ReadFromJsonAsync<ProblemDetails>();
        detail.ShouldNotBeNull();
        detail.Title.ShouldBe(StockErrors.StockNotFound.Code);

    }

    [Fact]
    public async Task AddStockItem_ReturnsStockItemAlreadyExists_WhenStockHasTheItem()
    {

        var stockItemId = Guid.CreateVersion7();
        var stock = await InsertStockAsync(new Stock(Guid.CreateVersion7(), new(), [new StockItem(stockItemId)]));

        var request = new AddStockItemRequest(stock.Id, stockItemId, 10, 10);
        var response = await _client.PostAsJsonAsync(ApiEndpoints.StockEndpoints.AddItem, request);

        response.ShouldNotBeNull();
        response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        var detail = await response.Content.ReadFromJsonAsync<ProblemDetails>();
        detail.ShouldNotBeNull();
        detail.Title.ShouldBe(StockErrors.StockItemAlreadyExists.Code);
    }

    [Theory]
    [ClassData(typeof(InvalidGuidData))]
    public async Task AddStockItem_ReturnsValidationError_WhenGivenStockIdIsInvalid(string invalidId)
    {
        var request = new AddStockItemRequest(Guid.Parse(invalidId), Guid.CreateVersion7(), 10, 10);

        var response = await _client.PostAsJsonAsync(ApiEndpoints.StockEndpoints.AddItem, request);

        response.ShouldNotBeNull();
        response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        var detail = await response.Content.ReadFromJsonAsync<ValidationProblemDetails>();
        detail.ShouldNotBeNull();
        detail.Errors.ShouldContainKey(nameof(request.StockId));
    }
    [Fact]
    public async Task AddStockItem_ReturnsValidationError_WhenGivenInitialQuantityGreaterThenInitialCapacity()
    {
        var request = new AddStockItemRequest(Guid.CreateVersion7(), Guid.CreateVersion7(), InitialQuantity: 15, InitialCapacity: 10);

        var response = await _client.PostAsJsonAsync(ApiEndpoints.StockEndpoints.AddItem, request);

        response.ShouldNotBeNull();
        response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        var detail = await response.Content.ReadFromJsonAsync<ValidationProblemDetails>();
        detail.ShouldNotBeNull();

    }

    [Fact]
    public async Task AddStockItem_ReturnsValidationError_WhenGivenRequestIsNotValid()
    {
        var request = new AddStockItemRequest(Guid.Empty, Guid.Empty, 0, 0);

        var response = await _client.PostAsJsonAsync(ApiEndpoints.StockEndpoints.AddItem, request);

        response.ShouldNotBeNull();
        response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        var detail = await response.Content.ReadFromJsonAsync<ValidationProblemDetails>();
        detail.ShouldNotBeNull();

    }
}
