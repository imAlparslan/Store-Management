using StoreDefinition.Contracts.Shops;

namespace StoreDefinition.Api.Tests.ShopsController;

[Collection(nameof(ShopsControllerCollectionFixture))]
public class AddGroupToShopControllerTests(StoreDefinitionApiFactory apiFactory)
    : ControllerTestBase(apiFactory)
{
    [Fact]
    public async Task AddGroupToShop_ReturnsShop_WhenGroupAdded()
    {
        var shop = await InsertShop();
        var groupId = Guid.NewGuid();
        var request = ShopsRequestFactory.CreateAddGroupToShopRequest(groupId);

        var response = await _client.PostAsJsonAsync($"{ShopsBaseAddress}/{shop.Id}/add-group", request);

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var shopResponse = await response.Content.ReadFromJsonAsync<ShopResponse>();
        shopResponse!.Groups.Should().Contain(groupId);
    }

    [Fact]
    public async Task AddGroupToShop_ReturnsNotFound_WhenShopNotExists()
    {
        var request = ShopsRequestFactory.CreateAddGroupToShopRequest(Guid.NewGuid());

        var response = await _client.PostAsJsonAsync($"{ShopsBaseAddress}/{Guid.NewGuid()}/add-group", request);

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
    [Fact]
    public async Task AddGroupToShop_ReturnBadRequest_WhenGroupIdInvalid()
    {
        var shop = await InsertShop();
        var request = ShopsRequestFactory.CreateAddGroupToShopRequest(Guid.Empty);

        var response = await _client.PostAsJsonAsync($"{ShopsBaseAddress}/{shop.Id}/add-group", request);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task AddGroupToShop_AddsShopToGroup_WhenGroupAdded()
    {
        var shop = await InsertShop();
        var group = await InsertGroup();
        var request = ShopsRequestFactory.CreateAddGroupToShopRequest(group.Id);

        var response = await _client.PostAsJsonAsync($"{ShopsBaseAddress}/{shop.Id}/add-group", request);

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var groupResponse = await _client.GetFromJsonAsync<GroupResponse>($"{GroupsBaseAddress}/{group.Id}");
        groupResponse!.ShopsIds.Should().Contain(shop.Id);
    }
}
