using FluentAssertions;
using NSubstitute;
using StoreDefinition.Application.Common.Repositories;
using StoreDefinition.Application.Shops.Queries.GetShopsByGroupId;
using StoreDefinition.Application.Tests.Common.Factories.ShopFactories;
using StoreDefinition.Domain.GroupAggregateRoot.ValueObjects;

namespace StoreDefinition.Application.Tests.Shops.QueryHandlers;
public class GetShopsByGroupIdQueryHandlerTests
{
    private readonly IShopRepository shopRepository;
    private readonly GetShopsByGroupIdQueryHandler handler;

    public GetShopsByGroupIdQueryHandlerTests()
    {
        shopRepository = Substitute.For<IShopRepository>();
        handler = new GetShopsByGroupIdQueryHandler(shopRepository);
    }

    [Fact]
    public async Task Handler_ReturnsShops_WhenShopHasGroupId()
    {
        var searchShopId = Guid.NewGuid();
        var nonSearchShopId = Guid.NewGuid();
        var shopHasSearchId1 = ShopFactory.CreateValid();
        shopHasSearchId1.AddGroup(searchShopId);
        shopHasSearchId1.AddGroup(nonSearchShopId);
        var shopHasSearchId2 = ShopFactory.CreateValid();
        shopHasSearchId2.AddGroup(searchShopId);
        shopRepository.GetShopsByGroupIdAsync(Arg.Any<GroupId>()).Returns([shopHasSearchId1, shopHasSearchId2]);
        var query = new GetShopsByGroupIdQuery(searchShopId);

        var result = await handler.Handle(query, default);

        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNullOrEmpty();
        result.Value.Should().Contain([shopHasSearchId1, shopHasSearchId2]);
    }


    [Fact]
    public async Task Handler_ReturnsEmptyCollection_WhenNoShopNotExists()
    {
        var searchShopId = Guid.NewGuid();
        shopRepository.GetShopsByGroupIdAsync(Arg.Any<GroupId>()).Returns([]);
        var query = new GetShopsByGroupIdQuery(searchShopId);

        var result = await handler.Handle(query, default);

        result.IsSuccess.Should().BeTrue();
        result.Value.Should().BeEmpty();
    }
}
