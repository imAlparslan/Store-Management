using StoreDefinition.Application.Groups.Commands.AddShopToGroup;
using StoreDefinition.Application.Groups.Commands.CreateGroup;
using StoreDefinition.Application.Groups.Commands.RemoveShopFromGroup;
using StoreDefinition.Application.Groups.Commands.UpdateGroup;
using StoreDefinition.Contracts.Groups;
using StoreDefinition.Domain.GroupAggregateRoot;

namespace StoreDefinition.Api.Mapping;

public static class GroupsMapping
{
    public static CreateGroupCommand MapToCommand(this CreateGroupRequest request)
        => new CreateGroupCommand(request.Name, request.Description);

    public static UpdateGroupCommand MapToCommand(this UpdateGroupRequest request, Guid id)
        => new UpdateGroupCommand(id, request.Name, request.Description);

    public static AddShopToGroupCommand MapToCommand(this AddShopToGroupRequest request, Guid groupId)
        => new AddShopToGroupCommand(groupId, request.ShopId);

    public static RemoveShopFromGroupCommand MapToCommand(this RemoveShopFromGroupRequest request, Guid groupId)
        => new RemoveShopFromGroupCommand(groupId, request.ShopId);

    public static GroupResponse MapToResponse(this Group group)
        => new GroupResponse(group.Id, group.Name.Value, group.Description.Value, group.ShopIds);
}
