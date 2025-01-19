using StoreDefinition.Application.Common.Interfaces;
using StoreDefinition.Application.Common.Repositories;
using StoreDefinition.Domain.ShopAggregateRoot.Events;

namespace StoreDefinition.Application.Groups.Events;
internal sealed class GroupRemovedFromShopEventHandler(IGroupRepository groupRepository) : IDomainEventHandler<GroupRemovedFromShopDomainEvent>
{
    private readonly IGroupRepository groupRepository = groupRepository;
    public async Task Handle(GroupRemovedFromShopDomainEvent notification, CancellationToken cancellationToken)
    {
        var group = await groupRepository.GetGroupByIdAsync(notification.GroupId, cancellationToken);
        if (group is not null)
        {
            group.RemoveShop(notification.ShopId);
            await groupRepository.UpdateGroupAsync(group, cancellationToken);
        }
    }
}
