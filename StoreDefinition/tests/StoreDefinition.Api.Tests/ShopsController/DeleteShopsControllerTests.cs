
namespace StoreDefinition.Api.Tests.ShopsController;


[Collection(nameof(ShopsControllerCollectionFixture))]
public class DeleteShopsControllerTests(StoreDefinitionApiFactory apiFactory)
    : ControllerTestBase(apiFactory)
{
    [Fact]
    public async Task Delete_ReturnsOk_WhenShopDeleted()
    {
        var shop = await InsertShop();

        var response = await _client.DeleteAsync($"{ShopsBaseAddress}/{shop.Id}");

        response.StatusCode.ShouldBe(HttpStatusCode.OK);
        var result = await response.Content.ReadFromJsonAsync<bool>();
        result.ShouldBeTrue();
    }

    [Fact]
    public async Task Delete_ReturnsNotFound_WhenShopNotExists()
    {
        var response = await _client.DeleteAsync($"{ShopsBaseAddress}/{Guid.NewGuid()}");

        response.StatusCode.ShouldBe(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task Delete_ReturnsValidationErrors_WhenIdInvalid()
    {
        var response = await _client.DeleteAsync($"{ShopsBaseAddress}/{Guid.Empty}");

        response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
    }
    [Fact]
    public async Task Delete_DeletesShopFromGroup_WhenShopDeleted()
    {
        var shop = await InsertShop();
        var group = await InsertGroup();
        await AddShopToGroup(shop.Id, group.Id);

        var response = await _client.DeleteAsync($"{ShopsBaseAddress}/{shop.Id}");

        response.StatusCode.ShouldBe(HttpStatusCode.OK);
        var groupResponse = await _client.GetFromJsonAsync<GroupResponse>($"{GroupsBaseAddress}/{group.Id}");
        groupResponse.ShouldNotBeNull();
        groupResponse.ShopsIds.ShouldNotContain(shop.Id);
    }
}