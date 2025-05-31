using StoreDefinition.Contracts.Shops;

namespace StoreDefinition.Api.Tests.ShopsController;

[Collection(nameof(ShopsControllerCollectionFixture))]

public class GetByIdControllerTests(StoreDefinitionApiFactory apiFactory)
    : ControllerTestBase(apiFactory)
{
    [Fact]
    public async Task GetById_ReturnsShopResponse_WhenShopExists()
    {
        var shop = await InsertShop();

        var response = await _client.GetAsync($"{ShopsBaseAddress}/{shop.Id}");

        response.StatusCode.ShouldBe(HttpStatusCode.OK);
        var shopResponse = await response.Content.ReadFromJsonAsync<ShopResponse>();
        shopResponse.ShouldNotBeNull();
        shopResponse.Id.ShouldBe(shop.Id);
        ;
    }

    [Fact]
    public async Task GetById_ReturnsNotFound_WhenShopNotExists()
    {
        var response = await _client.GetAsync($"{ShopsBaseAddress}/{Guid.NewGuid()}");

        response.StatusCode.ShouldBe(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task GetById_ReturnsValidationError_WhenIdInvalid()
    {
        var response = await _client.GetAsync($"{ShopsBaseAddress}/{Guid.Empty}");

        response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        var error = await response.Content.ReadFromJsonAsync<ValidationProblemDetails>();
        error.ShouldNotBeNull();
        error.Errors.ShouldHaveSingleItem();
    }
}
