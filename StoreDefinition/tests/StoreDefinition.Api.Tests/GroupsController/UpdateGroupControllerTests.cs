namespace StoreDefinition.Api.Tests.GroupsController;

[Collection(nameof(GroupsControllerCollectionFixture))]
public class UpdateGroupControllerTests(StoreDefinitionApiFactory apiFactory)
    : ControllerTestBase(apiFactory)
{

    [Fact]
    public async Task Update_ReturnsGroupResponse_WhenGroupUpdated()
    {
        var group = await InsertGroup();
        var request = GroupRequestFactory.CreateGroupUpdateRequest();

        var response = await _client.PutAsJsonAsync($"{GroupsBaseAddress}/{group!.Id}", request);

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var groupResponse = await response.Content.ReadFromJsonAsync<GroupResponse>();
        groupResponse.Should().NotBeNull();
    }

    [Fact]
    public async Task Update_ReturnsNotFound_WhenGroupNotFound()
    {
        var request = GroupRequestFactory.CreateGroupUpdateRequest();

        var response = await _client.PutAsJsonAsync($"{GroupsBaseAddress}/{Guid.NewGuid()}", request);

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task Update_ReturnsBadRequest_WhenGroupIdInvalid()
    {
        var request = GroupRequestFactory.CreateGroupUpdateRequest();

        var response = await _client.PutAsJsonAsync($"{GroupsBaseAddress}/{Guid.Empty}", request);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Theory]
    [MemberData(nameof(invalidStrings))]
    public async Task Update_ReturnsBadRequest_WhenGroupNameInvalid(string invalid)
    {
        var group = await InsertGroup();
        var request = GroupRequestFactory.CreateGroupUpdateRequest(groupName: invalid);

        var response = await _client.PutAsJsonAsync($"{GroupsBaseAddress}/{group!.Id}", request);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        var error = await response.Content.ReadFromJsonAsync<ValidationProblemDetails>();
        error.Should().NotBeNull();
        error!.Errors.Should().HaveCount(1);
    }

    [Theory]
    [MemberData(nameof(invalidStrings))]
    public async Task Update_ReturnsBadRequest_WhenGroupDescriptionInvalid(string invalid)
    {
        var group = await InsertGroup();
        var request = GroupRequestFactory.CreateGroupUpdateRequest(groupDescription: invalid);

        var response = await _client.PutAsJsonAsync($"{GroupsBaseAddress}/{group!.Id}", request);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        var error = await response.Content.ReadFromJsonAsync<ValidationProblemDetails>();
        error.Should().NotBeNull();
        error!.Errors.Should().HaveCount(1);
    }

    [Fact]
    public async Task Update_ReturnsBadRequest_WhenRequestInvalid()
    {
        var group = await InsertGroup();
        var request = GroupRequestFactory.CreateGroupUpdateRequest(groupName: "", groupDescription: "");

        var response = await _client.PutAsJsonAsync($"{GroupsBaseAddress}/{group!.Id}", request);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        var error = await response.Content.ReadFromJsonAsync<ValidationProblemDetails>();
        error.Should().NotBeNull();
        error!.Errors.Should().HaveCount(2);
    }
}
