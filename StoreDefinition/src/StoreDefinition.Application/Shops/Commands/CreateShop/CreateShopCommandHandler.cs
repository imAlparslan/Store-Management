using StoreDefinition.Application.Common.Interfaces;
using StoreDefinition.Application.Common.Repositories;
using StoreDefinition.Domain.ShopAggregateRoot;
using StoreDefinition.Domain.ShopAggregateRoot.Entities;
using StoreDefinition.Domain.ShopAggregateRoot.ValueObjects;
using StoreDefinition.SharedKernel;

namespace StoreDefinition.Application.Shops.Commands.CreateShop;
internal sealed class CreateShopCommandHandler(IShopRepository shopRepository)
        : ICommandHandler<CreateShopCommand, Result<Shop>>
{
    private readonly IShopRepository _shopRepository = shopRepository;

    public async Task<Result<Shop>> Handle(CreateShopCommand request, CancellationToken cancellationToken)
    {
        var shopDescription = new ShopDescription(request.Description);
        var address = new ShopAddress(request.City, request.Street);

        //TODO: Check groupIds

        var shop = Shop.CreateNew(shopDescription, address, request.GroupIds);

        await _shopRepository.InsertShopAsync(shop, cancellationToken);

        return shop;
    }
}
