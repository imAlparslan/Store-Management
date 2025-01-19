using StoreDefinition.Application.Common.Interfaces;
using StoreDefinition.Application.Common.Repositories;
using StoreDefinition.Domain.GroupAggregateRoot.Events;

namespace StoreDefinition.Application.Shops.Events;
internal sealed class ShopRemovedFromGroupDomainEventHandler(IShopRepository shopRepository) : IDomainEventHandler<ShopRemovedFromGroupDomainEvent>
{
    private readonly IShopRepository shopRepository = shopRepository;
    public async Task Handle(ShopRemovedFromGroupDomainEvent notification, CancellationToken cancellationToken)
    {
        var shop = await shopRepository.GetShopByIdAsync(notification.ShopId);
        if (shop is not null)
        {
            shop.RemoveGroup(notification.GroupId);
            await shopRepository.UpdateShopAsync(shop, cancellationToken);
        }
    }
}
