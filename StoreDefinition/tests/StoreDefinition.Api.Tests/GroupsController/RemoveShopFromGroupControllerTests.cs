using StoreDefinition.Contracts.Shops;

namespace StoreDefinition.Api.Tests.GroupsController;

[Collection(nameof(GroupsControllerCollectionFixture))]
public class RemoveShopFromGroupControllerTests(StoreDefinitionApiFactory apiFactory)
    : ControllerTestBase(apiFactory), IAsyncLifetime
{
    [Fact]
    public async Task RemoveShopFromGroup_ReturnsGroupResponse_WhenShopRemoved()
    {
        var group = await InsertGroup();
        var shop = await InsertShop();
        await AddShopToGroup(shop.Id, group.Id);
        var request = GroupRequestFactory.CreateRemoveShopFromGroupRequest(shop.Id);
        
        var response = await _client.PostAsJsonAsync($"{GroupsBaseAddress}/{group.Id}/remove-shop", request);
        
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }
    [Fact]
    public async Task RemoveShopFromGroup_ReturnsNotFound_WhenGroupNotFound()
    {
        var shop = await InsertShop();
        var request = GroupRequestFactory.CreateRemoveShopFromGroupRequest(shop.Id);

        var response = await _client.PostAsJsonAsync($"{GroupsBaseAddress}/{Guid.NewGuid()}/remove-shop", request);

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
    [Fact]
    public async Task RemoveShopFromGroup_ReturnsBadRequest_WhenGroupIdInvalid()
    {
        var request = GroupRequestFactory.CreateRemoveShopFromGroupRequest(Guid.NewGuid());

        var response = await _client.PostAsJsonAsync($"{GroupsBaseAddress}/{Guid.Empty}/remove-shop", request);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task RemoveShopFromGroup_RemovesGroupFromShop_WhenShopRemoved()
    {
        var group = await InsertGroup();
        var shop = await InsertShop();
        await AddShopToGroup(shop.Id, group.Id);
        var request = GroupRequestFactory.CreateRemoveShopFromGroupRequest(shop.Id);
        
        var response = await _client.PostAsJsonAsync($"{GroupsBaseAddress}/{group.Id}/remove-shop", request);
        
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var shopResponse = await _client.GetFromJsonAsync<ShopResponse>($"{ShopsBaseAddress}/{shop.Id}");
        shopResponse!.Groups.Should().NotContain(group.Id);
    }

}
