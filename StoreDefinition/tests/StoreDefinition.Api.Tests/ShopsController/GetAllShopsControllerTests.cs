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

        response.StatusCode.ShouldBe(HttpStatusCode.OK);
        var shops = await response.Content.ReadFromJsonAsync<List<ShopResponse>>();
        shops.ShouldNotBeNull();
        shops.Count.ShouldBe(2);
        shops.Select(x => x.Id).ShouldBeSubsetOf([shop1.Id, shop2.Id]);
    }

    [Fact]
    public async Task GetAllShops_ReturnsEmptyCollection_WhenNoShopsExists()
    {
        var response = await _client.GetAsync(ShopsBaseAddress);

        response.StatusCode.ShouldBe(HttpStatusCode.OK);
        var shops = await response.Content.ReadFromJsonAsync<List<ShopResponse>>();
        shops.ShouldBeEmpty();
    }
}
