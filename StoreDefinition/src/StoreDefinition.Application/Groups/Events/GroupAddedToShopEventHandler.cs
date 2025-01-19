using StoreDefinition.Application.Common.Interfaces;
using StoreDefinition.Application.Common.Repositories;
using StoreDefinition.Domain.ShopAggregateRoot.Events;

namespace StoreDefinition.Application.Groups.Events;
internal sealed class GroupAddedToShopEventHandler(IGroupRepository groupRepository) 
    : IDomainEventHandler<GroupAddedToShopDomainEvent>
{
    private readonly IGroupRepository groupRepository = groupRepository;
    public async Task Handle(GroupAddedToShopDomainEvent notification, CancellationToken cancellationToken)
    {
        var group = await groupRepository.GetGroupByIdAsync(notification.GroupId, cancellationToken);
        if (group is not null)
        {
            group.AddShop(notification.ShopId);
            await groupRepository.UpdateGroupAsync(group, cancellationToken);
        }
    }
}
