using StoreDefinition.Application.Common.Interfaces;
using StoreDefinition.Application.Common.Repositories;
using StoreDefinition.Domain.ShopAggregateRoot;
using StoreDefinition.SharedKernel;

namespace StoreDefinition.Application.Shops.Queries.GetShopsByGroupId;
internal sealed class GetShopsByGroupIdQueryHandler(IShopRepository shopRepository)
    : IQueryHandler<GetShopsByGroupIdQuery, Result<IEnumerable<Shop>>>
{
    private readonly IShopRepository shopRepository = shopRepository;
    public async Task<Result<IEnumerable<Shop>>> Handle(GetShopsByGroupIdQuery request, CancellationToken cancellationToken)
    {
        var result = await shopRepository.GetShopsByGroupIdAsync(request.GroupId, cancellationToken);
        return Result<IEnumerable<Shop>>.Success(result);
    }
}
