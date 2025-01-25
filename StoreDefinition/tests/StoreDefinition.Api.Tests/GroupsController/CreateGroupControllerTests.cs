namespace StoreDefinition.Api.Tests.GroupsController;

[Collection(nameof(GroupsControllerCollectionFixture))]
public class CreateGroupControllerTests(StoreDefinitionApiFactory apiFactory)
    : ControllerTestBase(apiFactory), IAsyncLifetime
{

    [Fact]
    public async Task Create_ReturnsGroupResponse_WhenGroupCreate()
    {
        var request = GroupRequestFactory.CreateGroupCreateRequest();

        var response = await _client.PostAsJsonAsync(GroupsBaseAddress, request);

        response.StatusCode.Should().Be(HttpStatusCode.Created);
        var groupResponse = await response.Content.ReadFromJsonAsync<GroupResponse>();
        groupResponse.Should().NotBeNull();
        response.Headers.Location.Should().Be($"{GroupsBaseAddress}/{groupResponse!.Id}");
    }

    [Theory]
    [MemberData(nameof(invalidStrings))]
    public async Task Create_ReturnsBadRequest_WhenGroupDescriptionInvalid(string invalid)
    {
        var request = GroupRequestFactory.CreateGroupCreateRequest(groupDescription: invalid);

        var response = await _client.PostAsJsonAsync(GroupsBaseAddress, request);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        var error = await response.Content.ReadFromJsonAsync<ValidationProblemDetails>();
        error.Should().NotBeNull();
        error!.Errors.Should().HaveCount(1);
    }
    [Theory]
    [MemberData(nameof(invalidStrings))]
    public async Task Create_ReturnsBadRequest_WhenGroupNameInvalid(string invalid)
    {
        var request = GroupRequestFactory.CreateGroupCreateRequest(groupName: invalid);

        var response = await _client.PostAsJsonAsync(GroupsBaseAddress, request);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        var error = await response.Content.ReadFromJsonAsync<ValidationProblemDetails>();
        error.Should().NotBeNull();
        error!.Errors.Should().HaveCount(1);
    }

    [Fact]
    public async Task Create_ReturnsBadRequest_WhenRequestInvalid()
    {
        var request = GroupRequestFactory.CreateGroupCreateRequest(groupName: "", groupDescription: "");
        var response = await _client.PostAsJsonAsync(GroupsBaseAddress, request);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        var error = await response.Content.ReadFromJsonAsync<ValidationProblemDetails>();
        error.Should().NotBeNull();
        error!.Errors.Should().HaveCount(2);
    }
}
