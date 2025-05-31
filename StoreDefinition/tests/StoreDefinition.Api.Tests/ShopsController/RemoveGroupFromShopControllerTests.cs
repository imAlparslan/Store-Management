using StoreDefinition.Contracts.Shops;

namespace StoreDefinition.Api.Tests.ShopsController;

[Collection(nameof(ShopsControllerCollectionFixture))]
public class RemoveGroupFromShopControllerTests(StoreDefinitionApiFactory apiFactory)
    : ControllerTestBase(apiFactory)
{
    [Fact]
    public async Task RemoveGroupFromShop_ReturnsShopResponse_WhenGroupRemoved()
    {
        var shop = await InsertShop();
        var group = await InsertGroup();
        await AddShopToGroup(shop.Id, group.Id);
        var request = ShopsRequestFactory.CreateRemoveGroupFromShopRequest(group.Id);

        var response = await _client.PostAsJsonAsync($"{ShopsBaseAddress}/{shop.Id}/remove-group", request);

        response.StatusCode.ShouldBe(HttpStatusCode.OK);
        var shopResponse = await response.Content.ReadFromJsonAsync<ShopResponse>();
        shopResponse.ShouldNotBeNull();
        shopResponse.Groups.ShouldNotContain(group.Id);

    }

    [Fact]
    public async Task RemoveGroupFromShop_ReturnsBadRequest_WhenGroupIdInvalid()
    {
        var shop = await InsertShop();
        var request = ShopsRequestFactory.CreateRemoveGroupFromShopRequest(Guid.Empty);

        var response = await _client.PostAsJsonAsync($"{ShopsBaseAddress}/{shop.Id}/remove-group", request);

        response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task RemoveGroupFromShop_ReturnsNotFound_WhenShopNotExists()
    {
        var request = ShopsRequestFactory.CreateRemoveGroupFromShopRequest(Guid.NewGuid());

        var response = await _client.PostAsJsonAsync($"{ShopsBaseAddress}/{Guid.NewGuid()}/remove-group", request);

        response.StatusCode.ShouldBe(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task RemoveGroupFromShop_RemovesShopFromGroup_WhenGroupRemoved()
    {
        var shop = await InsertShop();
        var group = await InsertGroup();
        await AddShopToGroup(shop.Id, group.Id);
        var request = ShopsRequestFactory.CreateRemoveGroupFromShopRequest(group.Id);

        var response = await _client.PostAsJsonAsync($"{ShopsBaseAddress}/{shop.Id}/remove-group", request);

        response.StatusCode.ShouldBe(HttpStatusCode.OK);
        var groupResponse = await _client.GetFromJsonAsync<GroupResponse>($"{GroupsBaseAddress}/{group.Id}");
        groupResponse.ShouldNotBeNull();
        groupResponse.ShopsIds.ShouldNotContain(shop.Id);
    }
}
