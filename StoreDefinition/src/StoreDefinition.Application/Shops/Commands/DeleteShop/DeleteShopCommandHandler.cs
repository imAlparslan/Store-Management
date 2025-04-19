using StoreDefinition.Application.Common.Repositories;
using StoreDefinition.Domain.ShopAggregateRoot.Errors;
using StoreDefinition.Domain.ShopAggregateRoot.Events;

namespace StoreDefinition.Application.Shops.Commands.DeleteShop;
internal sealed class DeleteShopCommandHandler(IShopRepository shopRepository) : ICommandHandler<DeleteShopCommand, Result<bool>>
{
    private readonly IShopRepository _shopRepository = shopRepository;

    public async Task<Result<bool>> Handle(DeleteShopCommand request, CancellationToken cancellationToken)
    {
        var result = await _shopRepository.GetShopByIdAsync(request.ShopId, cancellationToken);

        if (result is null)
        {
            return ShopErrors.NotFoundById;
        }

        result.AddDomainEvent(new ShopDeletedDomainEvent(request.ShopId));
        return await _shopRepository.DeleteShopByIdAsync(request.ShopId, cancellationToken);
    }
}
