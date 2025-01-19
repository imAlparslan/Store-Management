using StoreDefinition.Application.Common.Interfaces;
using StoreDefinition.Application.Common.Repositories;
using StoreDefinition.Domain.GroupAggregateRoot.Events;

namespace StoreDefinition.Application.Shops.Events;
internal class GroupDeletedDomainEventHandler(IShopRepository shopRepository) : IDomainEventHandler<GroupDeletedDomainEvent>
{
    private readonly IShopRepository shopRepository = shopRepository;
    public async Task Handle(GroupDeletedDomainEvent notification, CancellationToken cancellationToken)
    {
        var shops = await shopRepository.GetShopsByGroupIdAsync(notification.GroupId);
        //TODO: Unit of work
        foreach (var shop in shops)
        {
            shop.RemoveGroup(notification.GroupId);
            await shopRepository.UpdateShopAsync(shop, cancellationToken);
        }
    }
}
