using StoreDefinition.Application.Common.Interfaces;
using StoreDefinition.Application.Common.Repositories;
using StoreDefinition.Domain.ShopAggregateRoot;
using StoreDefinition.Domain.ShopAggregateRoot.Errors;
using StoreDefinition.SharedKernel;

namespace StoreDefinition.Application.Shops.Commands.AddGroupToShop;
internal sealed class AddGroupToShopCommandHandler(IShopRepository shopRepository) : ICommandHandler<AddGroupToShopCommand, Result<Shop>>
{
    private readonly IShopRepository _shopRepository = shopRepository;
    public async Task<Result<Shop>> Handle(AddGroupToShopCommand request, CancellationToken cancellationToken)
    {
        var shop = await _shopRepository.GetShopByIdAsync(request.ShopId, cancellationToken);
        if (shop is null)
        {
            return ShopErrors.NotFoundById;
        }
        //TODO: Check groups
        var result = shop.AddGroup(request.GroupId);

        if (result)
        {
            await _shopRepository.UpdateShopAsync(shop, cancellationToken);
            return shop;
        }

        return ShopErrors.GroupNotAddedToShop;
    }
}
