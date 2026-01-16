using Microsoft.AspNetCore.WebUtilities;
namespace InventoryManagement.Api.Tests.StocksController;

[Collection(nameof(StocksControllerCollectionFixture))]
public class GetAllStocksByGroupIdTests(InventoryApiFactory apiFactory) : ControllerTestBase(apiFactory)
{

    [Fact]
    public async Task GetAllStocksByGroupId_ReturnsStocksList_WhenStockHasTheGroupId8()
    {
        var groupId = Guid.CreateVersion7();
        var stock = new Stock(Guid.CreateVersion7(), [groupId], []);
        await InsertStockAsync(stock);
        var request = new GetAllStocksByGroupIdRequest(groupId);
        var queryParams = new Dictionary<string, string?>()
        {
            [nameof(request.GroupId)] = request.GroupId.ToString()
        };

        var uri = QueryHelpers.AddQueryString(StockEndpoints.GetAllStocksByGroupId, queryParams);

        var response = await _client.GetAsync(uri);

        response.ShouldNotBeNull();
        response.StatusCode.ShouldBe(HttpStatusCode.OK);
        var stockList = await response.Content.ReadFromJsonAsync<List<StockResponse>>();
        stockList.ShouldNotBeNull();
        stockList.ShouldHaveSingleItem();
        stockList.ShouldContain(x => x.Id == stock.Id);
    }
    [Fact]
    public async Task GetAllStocksByGroupId_ReturnsStockList_WhenMultipleStockHasTheGroupId()
    {
        var groupId = Guid.CreateVersion7();
        var stock1HasGroup = new Stock(Guid.CreateVersion7(), [groupId], []);
        await InsertStockAsync(stock1HasGroup);
        var stock2HasGroup = new Stock(Guid.CreateVersion7(), [groupId], []);
        await InsertStockAsync(stock2HasGroup);
        var stockHasNotGroup = new Stock(Guid.CreateVersion7(), [], []);
        await InsertStockAsync(stockHasNotGroup);
        var stockHasDifferentGroup = new Stock(Guid.CreateVersion7(), [], []);
        await InsertStockAsync(stockHasDifferentGroup);
        var request = new GetAllStocksByGroupIdRequest(groupId);
        var queryParams = new Dictionary<string, string?>()
        {
            [nameof(request.GroupId)] = request.GroupId.ToString()
        };
        var uri = QueryHelpers.AddQueryString(StockEndpoints.GetAllStocksByGroupId, queryParams);

        var response = await _client.GetAsync(uri);

        response.ShouldNotBeNull();
        response.StatusCode.ShouldBe(HttpStatusCode.OK);
        var stockList = await response.Content.ReadFromJsonAsync<List<StockResponse>>();
        stockList.ShouldNotBeNull();
        stockList.Select(x => x.Id).ShouldBeSubsetOf([stock1HasGroup.Id, stock2HasGroup.Id]);
        stockList.Select(x => x.Id).ShouldNotContain(stockHasNotGroup.Id);
        stockList.Select(x => x.Id).ShouldNotContain(stockHasDifferentGroup.Id);
    }

    [Fact]
    public async Task GetAllStocksByGroupId_ReturnsNotFound_WhenNoStocksExists()
    {
        var request = new GetAllStocksByGroupIdRequest(Guid.CreateVersion7());
        var queryParams = new Dictionary<string, string?>()
        {
            [nameof(request.GroupId)] = request.GroupId.ToString()
        };
        var uri = QueryHelpers.AddQueryString(StockEndpoints.GetAllStocksByGroupId, queryParams);

        var response = await _client.GetAsync(uri);

        response.StatusCode.ShouldBe(HttpStatusCode.NotFound);
        response.ShouldNotBeNull();
    }

    [Fact]
    public async Task GetAllStocksByGroupId_ReturnsNotFound_WhenNoStocksExistsWithGivenGroupId()
    {
        var stock1HasGroup = new Stock(Guid.CreateVersion7(), [Guid.CreateVersion7()], []);
        await InsertStockAsync(stock1HasGroup);
        var request = new GetAllStocksByGroupIdRequest(Guid.CreateVersion7());
        var queryParams = new Dictionary<string, string?>()
        {
            [nameof(request.GroupId)] = request.GroupId.ToString()
        };
        var uri = QueryHelpers.AddQueryString(StockEndpoints.GetAllStocksByGroupId, queryParams);

        var response = await _client.GetAsync(uri);

        response.StatusCode.ShouldBe(HttpStatusCode.NotFound);
        response.ShouldNotBeNull();
    }

    [Theory]
    [ClassData(typeof(InvalidGuidData))]
    public async Task GetAllStocksBuGroupId_ReturnsValidationError_WhenGroupIdInvalid(Guid invalidGuid)
    {
        var request = new GetAllStocksByGroupIdRequest(invalidGuid);
        var queryParams = new Dictionary<string, string?>()
        {
            [nameof(request.GroupId)] = request.GroupId.ToString()
        };
        var uri = QueryHelpers.AddQueryString(StockEndpoints.GetAllStocksByGroupId, queryParams);

        var response = await _client.GetAsync(uri);

        response.ShouldNotBeNull();
        response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        var detail = await response.Content.ReadFromJsonAsync<ValidationProblemDetails>();
        detail.ShouldNotBeNull();
        detail.Errors.ContainsKey(nameof(request.GroupId));
    }
}