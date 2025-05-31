using StoreDefinition.Application.Common.Repositories;
using StoreDefinition.Application.Groups.Queries.GetGroupsByShopId;
using StoreDefinition.Application.Tests.Common.Factories.GroupFactories;
using StoreDefinition.Domain.ShopAggregateRoot.ValueObjects;

namespace StoreDefinition.Application.Tests.Groups.QueryHandlers;
public class GetGroupsByShopIdQueryHandlerTests
{
    private readonly IGroupRepository groupRepository;
    private readonly GetGroupsByShopIdQueryHandler handler;

    public GetGroupsByShopIdQueryHandlerTests()
    {
        groupRepository = Substitute.For<IGroupRepository>();
        handler = new GetGroupsByShopIdQueryHandler(groupRepository);
    }

    [Fact]
    public async Task Handler_ReturnsGroupCollection_WhenGroupsExists()
    {
        var shopId = Guid.NewGuid();
        var groupHasShopId1 = GroupFactory.Create();
        groupHasShopId1.AddShop(shopId);
        var groupHasShopId2 = GroupFactory.Create();
        groupHasShopId2.AddShop(shopId);
        var groupHasShopId3 = GroupFactory.Create();
        groupHasShopId3.AddShop(shopId);
        groupRepository.GetGroupsByShopIdAsync(shopId).Returns([groupHasShopId1, groupHasShopId2, groupHasShopId3]);
        var query = new GetGroupsByShopIdQuery(shopId);

        var result = await handler.Handle(query, default);

        result.IsSuccess.ShouldBeTrue();
        result.Value.ShouldNotBeNull();
        result.Value.ShouldBeSubsetOf([groupHasShopId1, groupHasShopId2, groupHasShopId3]);
    }

    [Fact]
    public async Task Handler_ReturnsEmptyCollection_WhenGroupsNotExists()
    {
        groupRepository.GetGroupsByShopIdAsync(Arg.Any<ShopId>()).Returns([]);
        var query = new GetGroupsByShopIdQuery(Guid.NewGuid());

        var result = await handler.Handle(query, default);

        result.IsSuccess.ShouldBeTrue();
        result.Value.ShouldNotBeNull();
        result.Value.ShouldBeEmpty();
    }
}