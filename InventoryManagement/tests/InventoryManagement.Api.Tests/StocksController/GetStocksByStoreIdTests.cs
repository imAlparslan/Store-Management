using InventoryManagement.Api.Tests.Factories;
using Microsoft.AspNetCore.WebUtilities;

namespace InventoryManagement.Api.Tests.StocksController;

[Collection(nameof(StocksControllerCollectionFixture))]
public class GetStocksByStoreIdTests(InventoryApiFactory apiFactory) : ControllerTestBase(apiFactory)
{
    [Fact]
    public async Task GetStockByStoreId_ReturnsStock_WhenStoreIdMatch()
    {
        var insertedStock = await InsertStockAsync(StockFactory.CreateValid());
        var request = new GetStockByStoreIdRequest(insertedStock.StoreId);
        var queryParams = new Dictionary<string, string?>()
        {
            [nameof(request.StoreId)] = request.StoreId.ToString()
        };
        var uri = QueryHelpers.AddQueryString(StockEndpoints.GetStocksByStoreId, queryParams);

        var getStockByStoreIdResponse = await _client.GetAsync(uri);

        getStockByStoreIdResponse.ShouldNotBeNull();
        getStockByStoreIdResponse.StatusCode.ShouldBe(HttpStatusCode.OK);
        var stock = await getStockByStoreIdResponse.Content.ReadFromJsonAsync<StockResponse>();
        stock.ShouldNotBeNull();
        stock.Id.ShouldBe(insertedStock.Id.Value);
    }
    [Fact]
    public async Task GetStockByStoreId_ReturnsNotFoundError_WhenStoreIdNotMatch()
    {
        var request = new GetStockByStoreIdRequest(Guid.NewGuid());
        var queryParams = new Dictionary<string, string?>()
        {
            [nameof(request.StoreId)] = request.StoreId.ToString()
        };
        var uri = QueryHelpers.AddQueryString(StockEndpoints.GetStocksByStoreId, queryParams);

        var getStockByStoreIdResponse = await _client.GetAsync(uri);

        getStockByStoreIdResponse.ShouldNotBeNull();
        getStockByStoreIdResponse.StatusCode.ShouldBe(HttpStatusCode.NotFound);
    }
    [Theory]
    [ClassData(typeof(InvalidGuidData))]
    public async Task GetStockByStoreId_ReturnsValidationError_WhenStoreIdInvalid(string storeId)
    {
        var request = new GetStockByStoreIdRequest(Guid.Parse(storeId));
        var queryParams = new Dictionary<string, string?>()
        {
            [nameof(request.StoreId)] = request.StoreId.ToString()
        };
        var uri = QueryHelpers.AddQueryString(StockEndpoints.GetStocksByStoreId, queryParams);

        var getStockByStoreIdResponse = await _client.GetAsync(uri);

        getStockByStoreIdResponse.ShouldNotBeNull();
        getStockByStoreIdResponse.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        ValidationProblemDetails? problemDetail = await getStockByStoreIdResponse.Content.ReadFromJsonAsync<ValidationProblemDetails>();
        problemDetail.ShouldNotBeNull();
        problemDetail.Errors.Count.ShouldBe(1);
    }
}
