using StoreDefinition.Application.Common.Interfaces;
using StoreDefinition.Application.Common.Repositories;
using StoreDefinition.Domain.ShopAggregateRoot;
using StoreDefinition.Domain.ShopAggregateRoot.Errors;
using StoreDefinition.Domain.ShopAggregateRoot.Events;
using StoreDefinition.SharedKernel;

namespace StoreDefinition.Application.Shops.Commands.RemoveGroupFromShop;
internal sealed class RemoveGroupFromShopCommandHandler(IShopRepository shopRepository)
    : ICommandHandler<RemoveGroupFromShopCommand, Result<Shop>>
{
    private readonly IShopRepository shopRepository = shopRepository;
    public async Task<Result<Shop>> Handle(RemoveGroupFromShopCommand request, CancellationToken cancellationToken)
    {
        var shop = await shopRepository.GetShopByIdAsync(request.ShopId, cancellationToken);

        if (shop is null)
        {
            return ShopErrors.NotFoundById;
        }
        var result = shop.RemoveGroup(request.GroupId);
        if (result)
        {
            await shopRepository.UpdateShopAsync(shop, cancellationToken);
            return shop;
        }
        return ShopErrors.GroupNotRemovedFromShop;
    }
}
