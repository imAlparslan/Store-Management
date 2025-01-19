using StoreDefinition.Application.Common.Interfaces;
using StoreDefinition.Application.Common.Repositories;
using StoreDefinition.Domain.GroupAggregateRoot.Events;

namespace StoreDefinition.Application.Shops.Events;
internal sealed class ShopAddedToGroupDomainEventHandler(IShopRepository shopRepository) : IDomainEventHandler<ShopAddedToGroupDomainEvent>
{
    private readonly IShopRepository shopRepository = shopRepository;

    public async Task Handle(ShopAddedToGroupDomainEvent notification, CancellationToken cancellationToken)
    {
        var shop = await shopRepository.GetShopByIdAsync(notification.ShopId);
        if (shop is not null)
        {
            shop.AddGroup(notification.GroupId);
            await shopRepository.UpdateShopAsync(shop, cancellationToken);
        }
    }
}
