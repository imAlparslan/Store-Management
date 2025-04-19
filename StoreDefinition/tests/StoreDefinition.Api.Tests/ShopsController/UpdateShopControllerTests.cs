using FluentAssertions.Execution;
using StoreDefinition.Api.Tests.Common;
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
        using (AssertionScope scope = new())
        {
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var shopResponse = await response.Content.ReadFromJsonAsync<ShopResponse>();
            shopResponse.Should().NotBeNull();
        }
    }

    [Fact]
    public async Task Update_ReturnsNotFound_WhenShopNotExists()
    {
        UpdateShopRequest request = ShopsRequestFactory.UpdateShopCreateRequest();

        var response = await _client.PutAsJsonAsync($"{ShopsBaseAddress}/{Guid.NewGuid()}", request);
        using (AssertionScope scope = new())
        {
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
            var error = await response.Content.ReadFromJsonAsync<ValidationProblemDetails>();
            error.Should().NotBeNull();
        }
    }

    [Theory]
    [MemberData(nameof(invalidStrings))]
    public async Task Update_ReturnsUpdatedShop_WhenDescriptionValid(string invalid)
    {
        var insertedShop = await InsertShop();
        UpdateShopRequest request = ShopsRequestFactory.UpdateShopCreateRequest(shopDescription: invalid);

        var response = await _client.PutAsJsonAsync($"{ShopsBaseAddress}/{insertedShop.Id}", request);
        using (AssertionScope scope = new())
        {
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            var error = await response.Content.ReadFromJsonAsync<ValidationProblemDetails>();
            error.Should().NotBeNull();
            error!.Errors.Should().HaveCount(1);
        }
    }


    [Theory]
    [MemberData(nameof(invalidStrings))]
    public async Task Update_ReturnsUpdatedShop_WhenCityValid(string invalid)
    {
        var insertedShop = await InsertShop();
        UpdateShopRequest request = ShopsRequestFactory.UpdateShopCreateRequest(city: invalid);

        var response = await _client.PutAsJsonAsync($"{ShopsBaseAddress}/{insertedShop.Id}", request);
        using (AssertionScope scope = new())
        {
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            var error = await response.Content.ReadFromJsonAsync<ValidationProblemDetails>();
            error.Should().NotBeNull();
            error!.Errors.Should().HaveCount(1);
        }
    }

    [Theory]
    [MemberData(nameof(invalidStrings))]
    public async Task Update_ReturnsUpdatedShop_WhenStreetValid(string invalid)
    {
        var insertedShop = await InsertShop();
        UpdateShopRequest request = ShopsRequestFactory.UpdateShopCreateRequest(street: invalid);

        var response = await _client.PutAsJsonAsync($"{ShopsBaseAddress}/{insertedShop.Id}", request);
        using (AssertionScope scope = new())
        {
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            var error = await response.Content.ReadFromJsonAsync<ValidationProblemDetails>();
            error.Should().NotBeNull();
            error!.Errors.Should().HaveCount(1);
        }
    }

    [Fact]
    public async Task Update_ReturnsValidationErrors_WhenRequestValid()
    {
        var insertedShop = await InsertShop();
        UpdateShopRequest request = ShopsRequestFactory.UpdateShopCreateRequest("", "", "");

        var response = await _client.PutAsJsonAsync($"{ShopsBaseAddress}/{insertedShop.Id}", request);
        using (AssertionScope scope = new())
        {
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            var error = await response.Content.ReadFromJsonAsync<ValidationProblemDetails>();
            error.Should().NotBeNull();
            error!.Errors.Should().HaveCount(3);
        }
    }
}
