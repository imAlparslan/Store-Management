using StoreDefinition.Contracts.Groups;

namespace StoreDefinition.Api.Tests.Factories;
public static class GroupRequestFactory
{
    public static CreateGroupRequest CreateGroupCreateRequest(string? groupName = "Group Name", string? groupDescription = "Group Description")
        => new CreateGroupRequest(groupName, groupDescription);

    public static UpdateGroupRequest CreateGroupUpdateRequest(string? groupName = "Group Name", string? groupDescription = "Group Description")
       => new UpdateGroupRequest(groupName, groupDescription);

    public static AddShopToGroupRequest CreateAddShopToGroupRequest(Guid shopId)
        => new AddShopToGroupRequest(shopId);

    public static RemoveShopFromGroupRequest CreateRemoveShopFromGroupRequest(Guid shopId)
        => new RemoveShopFromGroupRequest(shopId);
}
