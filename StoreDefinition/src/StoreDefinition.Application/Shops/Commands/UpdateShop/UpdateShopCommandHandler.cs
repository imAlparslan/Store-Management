using StoreDefinition.Application.Common.Interfaces;
using StoreDefinition.Application.Common.Repositories;
using StoreDefinition.Domain.ShopAggregateRoot;
using StoreDefinition.Domain.ShopAggregateRoot.Entities;
using StoreDefinition.Domain.ShopAggregateRoot.Errors;
using StoreDefinition.Domain.ShopAggregateRoot.ValueObjects;
using StoreDefinition.SharedKernel;

namespace StoreDefinition.Application.Shops.Commands.UpdateShop;
internal sealed class UpdateShopCommandHandler(IShopRepository shopRepository)
    : ICommandHandler<UpdateShopCommand, Result<Shop>>
{
    private readonly IShopRepository _shopRepository = shopRepository;
    public async Task<Result<Shop>> Handle(UpdateShopCommand request, CancellationToken cancellationToken)
    {
        var shop = await _shopRepository.GetShopByIdAsync(request.ShopId, cancellationToken);

        if (shop is null)
        {
            return ShopErrors.NotFoundById;
        }
        shop.ChangeDescription(new ShopDescription(request.Description));
        shop.ChangeAddress(new ShopAddress(request.City, request.Street, shop.Address.Id));

        return await _shopRepository.UpdateShopAsync(shop, cancellationToken);
    }
}
