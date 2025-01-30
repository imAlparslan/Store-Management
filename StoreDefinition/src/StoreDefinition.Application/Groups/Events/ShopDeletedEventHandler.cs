using StoreDefinition.Application.Common.Interfaces;
using StoreDefinition.Application.Common.Repositories;
using StoreDefinition.Domain.ShopAggregateRoot.Events;

namespace StoreDefinition.Application.Groups.Events;
internal sealed class ShopDeletedEventHandler(IGroupRepository groupRepository) 
    : IDomainEventHandler<ShopDeletedDomainEvent>
{
    private readonly IGroupRepository groupRepository = groupRepository;
    public async Task Handle(ShopDeletedDomainEvent notification, CancellationToken cancellationToken)
    {
        var groups = await groupRepository.GetGroupsByShopIdAsync(notification.ShopId,cancellationToken);
        //TODO:UNIT OF WORK
        foreach (var group in groups)
        {
            group.RemoveShop(notification.ShopId);
            await groupRepository.UpdateGroupAsync(group, cancellationToken);
        }
    }
}
