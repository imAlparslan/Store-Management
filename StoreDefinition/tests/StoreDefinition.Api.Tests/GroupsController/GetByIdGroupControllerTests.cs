
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

        response.StatusCode.ShouldBe(HttpStatusCode.OK);
        var groupResponse = await response.Content.ReadFromJsonAsync<GroupResponse>();
        groupResponse.ShouldNotBeNull();
        groupResponse!.Id.ShouldBe(group.Id);
        groupResponse!.Name.ShouldBe(group.Name);
        groupResponse!.Description.ShouldBe(group.Description);
    }
    [Fact]
    public async Task GetById_ReturnsNotFound_WhenGroupNotFound()
    {
        var response = await _client.GetAsync($"{GroupsBaseAddress}/{Guid.NewGuid()}");

        response.StatusCode.ShouldBe(HttpStatusCode.NotFound);
    }
    [Fact]
    public async Task GetById_ReturnsBadRequest_WhenGroupIdInvalid()
    {
        var response = await _client.GetAsync($"{GroupsBaseAddress}/{Guid.Empty}");

        response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
    }
}