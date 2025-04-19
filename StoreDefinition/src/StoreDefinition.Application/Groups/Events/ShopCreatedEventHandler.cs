using StoreDefinition.Application.Common.Repositories;
using StoreDefinition.Domain.ShopAggregateRoot.Events;

namespace StoreDefinition.Application.Groups.Events;
internal sealed class ShopCreatedEventHandler(IGroupRepository groupRepository) : IDomainEventHandler<ShopCreatedDomainEvent>
{
    private readonly IGroupRepository _groupRepository = groupRepository;
    public async Task Handle(ShopCreatedDomainEvent notification, CancellationToken cancellationToken)
    {
        var groups = await _groupRepository.GetGroupsByIdsAsync(notification.Shop.GroupIds, cancellationToken);

        groups.ForEach(group =>
        {
            group.AddShop(notification.Shop.Id);
        });

        await _groupRepository.UpdateAll(groups, cancellationToken);
    }
}
