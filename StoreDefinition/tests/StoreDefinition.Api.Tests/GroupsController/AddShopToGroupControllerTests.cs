using StoreDefinition.Contracts.Shops;

namespace StoreDefinition.Api.Tests.GroupsController;

[Collection(nameof(GroupsControllerCollectionFixture))]
public class AddShopToGroupControllerTests(StoreDefinitionApiFactory apiFactory) : ControllerTestBase(apiFactory)
{
    [Fact]
    public async Task AddShopToGroup_ReturnsGroupResponse_WhenShopAdded()
    {
        var group = await InsertGroup();
        var shop = await InsertShop();
        var request = GroupRequestFactory.CreateAddShopToGroupRequest(shop.Id);
        var response = await _client.PostAsJsonAsync($"{GroupsBaseAddress}/{group.Id}/add-shop", request);

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var groupResponse = await response.Content.ReadFromJsonAsync<GroupResponse>();
        groupResponse.Should().NotBeNull();
        groupResponse!.ShopsIds.Should().ContainSingle(s => s == shop.Id);
    }

    [Fact]
    public async Task AddShopToGroup_ReturnsNotFound_WhenGroupNotFound()
    {
        var shop = await InsertShop();
        var request = GroupRequestFactory.CreateAddShopToGroupRequest(shop.Id);
        var response = await _client.PostAsJsonAsync($"{GroupsBaseAddress}/{Guid.NewGuid()}/add-shop", request);

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
    [Fact]
    public async Task AddShopToGroup_ReturnsBadRequest_WhenGroupIdInvalid()
    {
        var request = GroupRequestFactory.CreateAddShopToGroupRequest(Guid.NewGuid());
        var response = await _client.PostAsJsonAsync($"{GroupsBaseAddress}/{Guid.Empty}/add-shop", request);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task AddShopToGroup_AddsGroupToShop_WhenShopAdded()
    {
        var group = await InsertGroup();
        var shop = await InsertShop();
        var request = GroupRequestFactory.CreateAddShopToGroupRequest(shop.Id);
        
        var response = await _client.PostAsJsonAsync($"{GroupsBaseAddress}/{group.Id}/add-shop", request);
        
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var shopResponse = await _client.GetFromJsonAsync<ShopResponse>($"{ShopsBaseAddress}/{shop.Id}");
        shopResponse!.Groups.Should().Contain(group.Id);
    }
}
