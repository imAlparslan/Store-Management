using FluentAssertions;
using StoreDefinition.Api.Tests.Fixtures;
using System.Net;
using System.Net.Http.Json;

namespace StoreDefinition.Api.Tests.ShopsController;

[Collection(nameof(ShopsControllerCollectionFixture))]
public class DeleteShopsControllerTests(StoreDefinitionApiFactory apiFactory)
    : ControllerTestBase(apiFactory), IAsyncLifetime
{
    [Fact]
    public async Task Delete_ReturnsOk_WhenShopDeleted()
    {
        var shop = await InsertShop();

        var response = await _client.DeleteAsync($"{ShopsBaseAddress}/{shop.Id}");

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var result = await response.Content.ReadFromJsonAsync<bool>();
        result.Should().BeTrue();
    }

    [Fact]
    public async Task Delete_ReturnsNotFound_WhenShopNotExists()
    {
        var response = await _client.DeleteAsync($"{ShopsBaseAddress}/{Guid.NewGuid()}");

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task Delete_ReturnsValidationErrors_WhenIdInvalid()
    {
        var response = await _client.DeleteAsync($"{ShopsBaseAddress}/{Guid.Empty}");

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
    [Fact]
    public async Task Delete_DeletesShopFromGroup_WhenShopDeleted()
    {
        var shop = await InsertShop();
        var group = await InsertGroup();
        await AddShopToGroup(shop.Id, group.Id);
       
        var response = await _client.DeleteAsync($"{ShopsBaseAddress}/{shop.Id}");
        
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var groupResponse = await _client.GetFromJsonAsync<GroupResponse>($"{GroupsBaseAddress}/{group.Id}");
        groupResponse!.ShopsIds.Should().NotContain(shop.Id);
    }
}