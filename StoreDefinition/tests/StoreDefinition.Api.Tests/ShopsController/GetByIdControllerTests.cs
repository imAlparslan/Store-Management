using FluentAssertions.Execution;
using StoreDefinition.Api.Tests.Common;
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

        using (AssertionScope scope = new())
        {
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var shopResponse = await response.Content.ReadFromJsonAsync<ShopResponse>();
            shopResponse.Should().NotBeNull();
            shopResponse!.Id.Should().Be(shop.Id);
        }
        ;
    }

    [Fact]
    public async Task GetById_ReturnsNotFound_WhenShopNotExists()
    {
        var response = await _client.GetAsync($"{ShopsBaseAddress}/{Guid.NewGuid()}");

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task GetById_ReturnsValidationError_WhenIdInvalid()
    {
        var response = await _client.GetAsync($"{ShopsBaseAddress}/{Guid.Empty}");

        using (AssertionScope scope = new())
        {
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            var error = await response.Content.ReadFromJsonAsync<ValidationProblemDetails>();
            error.Should().NotBeNull();
            error.Errors.Should().HaveCount(1);
        }
        ;
    }
}
