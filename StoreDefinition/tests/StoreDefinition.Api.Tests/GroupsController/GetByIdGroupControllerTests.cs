using StoreDefinition.Api.Tests.Common;

namespace StoreDefinition.Api.Tests.GroupsController;

[Collection(nameof(GroupsControllerCollectionFixture))]
public class GetByIdGroupControllerTests(StoreDefinitionApiFactory apiFactory)
    : ControllerTestBase(apiFactory)
{
    [Fact]
    public async Task GetById_ReturnsGroupResponse_WhenGroupExists()
    {
        var group = await InsertGroup();

        var response = await _client.GetAsync($"{GroupsBaseAddress}/{group.Id}");

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var groupResponse = await response.Content.ReadFromJsonAsync<GroupResponse>();
        groupResponse.Should().NotBeNull();
        groupResponse!.Id.Should().Be(group.Id);
        groupResponse!.Name.Should().Be(group.Name);
        groupResponse!.Description.Should().Be(group.Description);
    }
    [Fact]
    public async Task GetById_ReturnsNotFound_WhenGroupNotFound()
    {
        var response = await _client.GetAsync($"{GroupsBaseAddress}/{Guid.NewGuid()}");

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
    [Fact]
    public async Task GetById_ReturnsBadRequest_WhenGroupIdInvalid()
    {
        var response = await _client.GetAsync($"{GroupsBaseAddress}/{Guid.Empty}");

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
}