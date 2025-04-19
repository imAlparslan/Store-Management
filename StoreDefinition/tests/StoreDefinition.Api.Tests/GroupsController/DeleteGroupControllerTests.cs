using StoreDefinition.Api.Tests.Common;
using StoreDefinition.Contracts.Shops;

namespace StoreDefinition.Api.Tests.GroupsController;

[Collection(nameof(GroupsControllerCollectionFixture))]
public class DeleteGroupControllerTests(StoreDefinitionApiFactory apiFactory) :
    ControllerTestBase(apiFactory)
{
    [Fact]
    public async Task Delete_ReturnsOk_WhenGroupDeleted()
    {
        var group = await InsertGroup();

        var response = await _client.DeleteAsync($"{GroupsBaseAddress}/{group.Id}");

        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task Delete_ReturnsNotFound_WhenGroupNotFound()
    {
        var response = await _client.DeleteAsync($"{GroupsBaseAddress}/{Guid.NewGuid()}");

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task Delete_ReturnsBadRequest_WhenGroupIdInvalid()
    {
        var response = await _client.DeleteAsync($"{GroupsBaseAddress}/{Guid.Empty}");
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
    [Fact]
    public async Task Delete_DeletesGroupFromShop_WhenGroupDeleted()
    {
        var group = await InsertGroup();
        var shop = await InsertShop();
        await AddShopToGroup(shop.Id, group.Id);

        var response = await _client.DeleteAsync($"{GroupsBaseAddress}/{group.Id}");

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var shopResponse = await _client.GetFromJsonAsync<ShopResponse>($"{ShopsBaseAddress}/{shop.Id}");
        shopResponse!.Groups.Should().NotContain(group.Id);
    }
}