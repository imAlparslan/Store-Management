using StoreDefinition.Application.Common.Repositories;
using StoreDefinition.Domain.ShopAggregateRoot.ValueObjects;
namespace StoreDefinition.InternalApi.Services;

public class StoreDefinitionService(IShopRepository shopRepository) : StoreDefinitionGrpc.StoreDefinitionGrpcBase
{
    private readonly IShopRepository _shopRepository = shopRepository;

    public override async Task<GetStoreGroupsByStoreIdReply> GetStoreGroupsByStoreId(GetStoreGroupsByStoreIdRequest request, ServerCallContext context)
    {
        ShopId shopId = ShopId.CreateFromString(request.ShopId.Value);

        var groups = await _shopRepository.GetShopGroupIdsByShopIdAsync(shopId);

        GetStoreGroupsByStoreIdReply replay = new GetStoreGroupsByStoreIdReply();

        replay.Ids.AddRange(groups.Select(x => x.ToProto()));

        return replay;
    }
}
