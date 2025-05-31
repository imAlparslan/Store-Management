using StoreDefinition.Contracts.Shops;

namespace StoreDefinition.Api.Tests.ShopsController;

[Collection(nameof(ShopsControllerCollectionFixture))]

public class UpdateShopControllerTests(StoreDefinitionApiFactory apiFactory)
    : ControllerTestBase(apiFactory)
{
    [Fact]
    public async Task Update_ReturnsUpdatedShop_WhenRequestValid()
    {
        var insertedShop = await InsertShop();
        UpdateShopRequest request = ShopsRequestFactory.UpdateShopCreateRequest();

        var response = await _client.PutAsJsonAsync($"{ShopsBaseAddress}/{insertedShop.Id}", request);

        response.StatusCode.ShouldBe(HttpStatusCode.OK);
        var shopResponse = await response.Content.ReadFromJsonAsync<ShopResponse>();
        shopResponse.ShouldNotBeNull();
    }

    [Fact]
    public async Task Update_ReturnsNotFound_WhenShopNotExists()
    {
        UpdateShopRequest request = ShopsRequestFactory.UpdateShopCreateRequest();

        var response = await _client.PutAsJsonAsync($"{ShopsBaseAddress}/{Guid.NewGuid()}", request);

        response.StatusCode.ShouldBe(HttpStatusCode.NotFound);
        var error = await response.Content.ReadFromJsonAsync<ValidationProblemDetails>();
        error.ShouldNotBeNull();
    }

    [Theory]
    [ClassData(typeof(InvalidStrings))]
    public async Task Update_ReturnsUpdatedShop_WhenDescriptionValid(string invalid)
    {
        var insertedShop = await InsertShop();
        UpdateShopRequest request = ShopsRequestFactory.UpdateShopCreateRequest(shopDescription: invalid);

        var response = await _client.PutAsJsonAsync($"{ShopsBaseAddress}/{insertedShop.Id}", request);

        response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        var error = await response.Content.ReadFromJsonAsync<ValidationProblemDetails>();
        error.ShouldNotBeNull();
        error.Errors.ShouldHaveSingleItem();
    }


    [Theory]
    [ClassData(typeof(InvalidStrings))]
    public async Task Update_ReturnsUpdatedShop_WhenCityValid(string invalid)
    {
        var insertedShop = await InsertShop();
        UpdateShopRequest request = ShopsRequestFactory.UpdateShopCreateRequest(city: invalid);

        var response = await _client.PutAsJsonAsync($"{ShopsBaseAddress}/{insertedShop.Id}", request);

        response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        var error = await response.Content.ReadFromJsonAsync<ValidationProblemDetails>();
        error.ShouldNotBeNull();
        error.Errors.ShouldHaveSingleItem();
    }

    [Theory]
    [ClassData(typeof(InvalidStrings))]
    public async Task Update_ReturnsUpdatedShop_WhenStreetValid(string invalid)
    {
        var insertedShop = await InsertShop();
        UpdateShopRequest request = ShopsRequestFactory.UpdateShopCreateRequest(street: invalid);

        var response = await _client.PutAsJsonAsync($"{ShopsBaseAddress}/{insertedShop.Id}", request);

        response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        var error = await response.Content.ReadFromJsonAsync<ValidationProblemDetails>();
        error.ShouldNotBeNull();
        error!.Errors.ShouldHaveSingleItem();
    }

    [Fact]
    public async Task Update_ReturnsValidationErrors_WhenRequestValid()
    {
        var insertedShop = await InsertShop();
        UpdateShopRequest request = ShopsRequestFactory.UpdateShopCreateRequest("", "", "");

        var response = await _client.PutAsJsonAsync($"{ShopsBaseAddress}/{insertedShop.Id}", request);

        response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        var error = await response.Content.ReadFromJsonAsync<ValidationProblemDetails>();
        error.ShouldNotBeNull();
        error.Errors.Count.ShouldBe(3);
    }
}
