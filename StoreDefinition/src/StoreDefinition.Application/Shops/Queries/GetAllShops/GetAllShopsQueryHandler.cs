using StoreDefinition.Application.Common.Interfaces;
using StoreDefinition.Application.Common.Repositories;
using StoreDefinition.Domain.ShopAggregateRoot;
using StoreDefinition.SharedKernel;

namespace StoreDefinition.Application.Shops.Queries.GetAllShops;
internal sealed class GetAllShopsQueryHandler(IShopRepository shopRepository)
    : IQueryHandler<GetAllShopsQuery, Result<IEnumerable<Shop>>>
{
    private readonly IShopRepository shopRepository = shopRepository;
    public async Task<Result<IEnumerable<Shop>>> Handle(GetAllShopsQuery request, CancellationToken cancellationToken)
    {
        var result = await shopRepository.GetAllShopsAsync(cancellationToken);
        return Result<IEnumerable<Shop>>.Success(result);
    }
}
