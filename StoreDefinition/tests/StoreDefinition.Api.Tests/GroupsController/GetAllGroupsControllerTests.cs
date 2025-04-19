using StoreDefinition.Api.Tests.Common;

namespace StoreDefinition.Api.Tests.GroupsController;

[Collection(nameof(GroupsControllerCollectionFixture))]
public class GetAllGroupsControllerTests(StoreDefinitionApiFactory apiFactory)
    : ControllerTestBase(apiFactory)
{
    [Fact]
    public async Task GetAll_ReturnsGroupsResponseCollection_WhenGroupsExist()
    {
        var group1 = await InsertGroup();
        var group2 = await InsertGroup();

        var response = await _client.GetAsync(GroupsBaseAddress);

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var groupsResponse = await response.Content.ReadFromJsonAsync<List<GroupResponse>>();
        groupsResponse.Should().NotBeNull();
        groupsResponse!.Should().HaveCount(2);
        groupsResponse!.Should().ContainSingle(g => g.Id == group1.Id);
        groupsResponse!.Should().ContainSingle(g => g.Id == group2.Id);
    }
    [Fact]
    public async Task GetAll_ReturnsEmptyList_WhenNoGroupsExist()
    {
        var response = await _client.GetAsync(GroupsBaseAddress);

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var groupsResponse = await response.Content.ReadFromJsonAsync<List<GroupResponse>>();
        groupsResponse.Should().NotBeNull();
        groupsResponse!.Should().BeEmpty();
    }
}
