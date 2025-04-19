using FluentAssertions.Execution;
using StoreDefinition.Contracts.Shops;

namespace StoreDefinition.Api.Tests.ShopsController;

[Collection(nameof(ShopsControllerCollectionFixture))]
public class GetAllShopsControllerTests(StoreDefinitionApiFactory apiFactory)
    : ControllerTestBase(apiFactory)
{
    [Fact]
    public async Task GetAllShops_ReturnsShopCollection_WhenShopsExists()
    {
        var shop1 = await InsertShop();
        var shop2 = await InsertShop();

        var response = await _client.GetAsync(ShopsBaseAddress);

        using (AssertionScope scope = new())
        {
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var shops = await response.Content.ReadFromJsonAsync<List<ShopResponse>>();
            shops.Should().HaveCount(2);
            shops!.Select(x => x.Id).Should().Contain([shop1.Id, shop2.Id]);
        }
    }

    [Fact]
    public async Task GetAllShops_ReturnsEmptyCollection_WhenNoShopsExists()
    {
        var response = await _client.GetAsync(ShopsBaseAddress);

        using (AssertionScope scope = new())
        {
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var shops = await response.Content.ReadFromJsonAsync<List<ShopResponse>>();
            shops.Should().HaveCount(0);
        }
    }
}
