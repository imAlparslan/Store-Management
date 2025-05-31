using StoreDefinition.Api.Tests.Common;

namespace StoreDefinition.Api.Tests.GroupsController;

[Collection(nameof(GroupsControllerCollectionFixture))]
public class CreateGroupControllerTests(StoreDefinitionApiFactory apiFactory)
    : ControllerTestBase(apiFactory)
{

    [Fact]
    public async Task Create_ReturnsGroupResponse_WhenGroupCreate()
    {
        var request = GroupRequestFactory.CreateGroupCreateRequest();

        var response = await _client.PostAsJsonAsync(GroupsBaseAddress, request);

        response.StatusCode.ShouldBe(HttpStatusCode.Created);
        var groupResponse = await response.Content.ReadFromJsonAsync<GroupResponse>();
        groupResponse.ShouldNotBeNull();
        response.Headers.Location.ShouldNotBeNull();
        response.Headers.Location.AbsoluteUri.ShouldBe($"{GroupsBaseAddress}/{groupResponse!.Id}");
    }

    [Theory]
    [ClassData(typeof(InvalidStrings))]
    public async Task Create_ReturnsBadRequest_WhenGroupDescriptionInvalid(string invalid)
    {
        var request = GroupRequestFactory.CreateGroupCreateRequest(groupDescription: invalid);

        var response = await _client.PostAsJsonAsync(GroupsBaseAddress, request);

        response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        var error = await response.Content.ReadFromJsonAsync<ValidationProblemDetails>();
        error.ShouldNotBeNull();
        error.Errors.ShouldHaveSingleItem();
    }
    [Theory]
    [ClassData(typeof(InvalidStrings))]
    public async Task Create_ReturnsBadRequest_WhenGroupNameInvalid(string invalid)
    {
        var request = GroupRequestFactory.CreateGroupCreateRequest(groupName: invalid);

        var response = await _client.PostAsJsonAsync(GroupsBaseAddress, request);

        response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        var error = await response.Content.ReadFromJsonAsync<ValidationProblemDetails>();
        error.ShouldNotBeNull();
        error.Errors.ShouldHaveSingleItem();
    }

    [Fact]
    public async Task Create_ReturnsBadRequest_WhenRequestInvalid()
    {
        var request = GroupRequestFactory.CreateGroupCreateRequest(groupName: "", groupDescription: "");
        var response = await _client.PostAsJsonAsync(GroupsBaseAddress, request);

        response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);

        var error = await response.Content.ReadFromJsonAsync<ValidationProblemDetails>();
        error.ShouldNotBeNull();
        error.Errors.Count().ShouldBe(2);
    }
}
