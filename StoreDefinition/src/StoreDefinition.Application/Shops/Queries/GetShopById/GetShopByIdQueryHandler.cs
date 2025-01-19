using StoreDefinition.Application.Common.Interfaces;
using StoreDefinition.Application.Common.Repositories;
using StoreDefinition.Domain.ShopAggregateRoot;
using StoreDefinition.Domain.ShopAggregateRoot.Errors;
using StoreDefinition.SharedKernel;

namespace StoreDefinition.Application.Shops.Queries.GetShopById;
internal sealed class GetShopByIdQueryHandler(IShopRepository shopRepository) : IQueryHandler<GetShopByIdQuery, Result<Shop>>
{
    private readonly IShopRepository shopRepository = shopRepository;
    public async Task<Result<Shop>> Handle(GetShopByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await shopRepository.GetShopByIdAsync(request.ShopId, cancellationToken);

        if (result is null)
        {
            return ShopErrors.NotFoundById;
        }
        return result;
    }
}
