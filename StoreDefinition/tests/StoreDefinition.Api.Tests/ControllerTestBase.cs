using StoreDefinition.Api.Tests.Factories;
using StoreDefinition.Contracts.Shops;
using System.Net.Http.Json;

namespace StoreDefinition.Api.Tests;
public abstract class ControllerTestBase : IAsyncLifetime
{
    protected readonly HttpClient _client;
    protected readonly Func<Task> _resetDatabase;
    public const string ShopsBaseAddress = "http://localhost/api/shops";
    public const string GroupsBaseAddress = "http://localhost/api/groups";

    protected ControllerTestBase(StoreDefinitionApiFactory apiFactory)
    {
        _client = apiFactory.client;
        _resetDatabase = apiFactory.ResetDb;

    }

    protected async Task<ShopResponse> InsertShop()
    {
        CreateShopRequest request = ShopsRequestFactory.CreateShopCreateRequest();
        var response = await _client.PostAsJsonAsync(ShopsBaseAddress, request);
        return await response.Content.ReadFromJsonAsync<ShopResponse>();
    }

    protected async Task<GroupResponse> InsertGroup()
    {
        CreateGroupRequest request = GroupRequestFactory.CreateGroupCreateRequest();
        var response = await _client.PostAsJsonAsync(GroupsBaseAddress, request);
        return await response.Content.ReadFromJsonAsync<GroupResponse>();
    }
    protected async Task AddShopToGroup(Guid shopId, Guid groupId)
    {
        var request = ShopsRequestFactory.CreateAddGroupToShopRequest(groupId);
        await _client.PostAsJsonAsync($"{ShopsBaseAddress}/{shopId}/add-group", request);
    }
    public static readonly TheoryData<string> invalidStrings = ["", " ", null];
    public async Task DisposeAsync() => await Task.CompletedTask;
    public async Task InitializeAsync() => await _resetDatabase();
}
