using StoreDefinition.Application.Common.Interfaces;
using StoreDefinition.Application.Common.Repositories;
using StoreDefinition.Domain.GroupAggregateRoot;
using StoreDefinition.Domain.GroupAggregateRoot.Errors;
using StoreDefinition.Domain.GroupAggregateRoot.Events;
using StoreDefinition.SharedKernel;

namespace StoreDefinition.Application.Groups.Commands.AddShopToGroup;
internal sealed class AddShopToGroupCommandHandler(IGroupRepository groupRepository) : ICommandHandler<AddShopToGroupCommand, Result<Group>>
{
    private readonly IGroupRepository groupRepository = groupRepository;
    public async Task<Result<Group>> Handle(AddShopToGroupCommand request, CancellationToken cancellationToken)
    {
        var group = await groupRepository.GetGroupByIdAsync(request.GroupId, cancellationToken);
        if (group is null)
        {
            return GroupErrors.NotFoundById;
        }

        var result = group.AddShop(request.ShopId);

        if (result)
        {
            group.AddDomainEvent(new ShopAddedToGroupDomainEvent(request.GroupId, request.ShopId));
            return await groupRepository.UpdateGroupAsync(group, cancellationToken);
        }
        return GroupErrors.ShopNotAddedToGroup;
    }
}