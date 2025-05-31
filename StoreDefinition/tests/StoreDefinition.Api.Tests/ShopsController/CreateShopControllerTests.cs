using StoreDefinition.Contracts.Shops;

namespace StoreDefinition.Api.Tests.ShopsController;

[Collection(nameof(ShopsControllerCollectionFixture))]
public class CreateShopControllerTests(StoreDefinitionApiFactory apiFactory)
    : ControllerTestBase(apiFactory)
{
    [Fact]
    public async Task Create_ReturnsCreatedProduct_WhenRequestValid()
    {
        CreateShopRequest request = ShopsRequestFactory.CreateShopCreateRequest();

        var response = await _client.PostAsJsonAsync(ShopsBaseAddress, request);

        response.StatusCode.ShouldBe(HttpStatusCode.Created);
        var shopResponse = await response.Content.ReadFromJsonAsync<ShopResponse>();
        shopResponse.ShouldNotBeNull();
        response.Headers.Location.ShouldNotBeNull();
        response.Headers.Location.AbsoluteUri.ShouldBe($"{ShopsBaseAddress}/{shopResponse!.Id}");
    }

    [Theory]
    [ClassData(typeof(InvalidStrings))]
    public async Task Create_ValidationError_WhenShopDescriptionInvalid(string invalid)
    {
        CreateShopRequest request = ShopsRequestFactory.CreateShopCreateRequest(shopDescription: invalid);

        var response = await _client.PostAsJsonAsync(ShopsBaseAddress, request);

        response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        var error = await response.Content.ReadFromJsonAsync<ValidationProblemDetails>();
        error.ShouldNotBeNull();
        error.Status.ShouldBe(400);
        error.Errors.ShouldHaveSingleItem();
    }


    [Theory]
    [ClassData(typeof(InvalidStrings))]
    public async Task Create_ValidationError_WhenShopCityInvalid(string invalid)
    {
        CreateShopRequest request = ShopsRequestFactory.CreateShopCreateRequest(city: invalid);

        var response = await _client.PostAsJsonAsync(ShopsBaseAddress, request);

        response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        var error = await response.Content.ReadFromJsonAsync<ValidationProblemDetails>();
        error.ShouldNotBeNull();
        error.Status.ShouldBe(400);
        error.Errors.ShouldHaveSingleItem();
    }


    [Theory]
    [ClassData(typeof(InvalidStrings))]
    public async Task Create_ValidationError_WhenShopStreetInvalid(string invalid)
    {
        CreateShopRequest request = ShopsRequestFactory.CreateShopCreateRequest(street: invalid);

        var response = await _client.PostAsJsonAsync(ShopsBaseAddress, request);

        response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        var error = await response.Content.ReadFromJsonAsync<ValidationProblemDetails>();
        error.ShouldNotBeNull();
        error.Status.ShouldBe(400);
        error.Errors.ShouldHaveSingleItem();
    }


    [Fact]
    public async Task Create_ValidationError_WhenShopRequestInvalid()
    {
        CreateShopRequest request = ShopsRequestFactory.CreateShopCreateRequest("", "", "");

        var response = await _client.PostAsJsonAsync(ShopsBaseAddress, request);

        response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        var error = await response.Content.ReadFromJsonAsync<ValidationProblemDetails>();
        error.ShouldNotBeNull();
        error!.Status.ShouldBe(400);
        error!.Errors.Count.ShouldBe(3);
    }
}
