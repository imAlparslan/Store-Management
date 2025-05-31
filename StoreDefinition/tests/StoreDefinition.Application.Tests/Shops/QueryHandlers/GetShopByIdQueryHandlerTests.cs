using NSubstitute.ReturnsExtensions;
using StoreDefinition.Application.Common.Repositories;
using StoreDefinition.Application.Shops.Queries.GetShopById;
using StoreDefinition.Application.Tests.Common.Factories.ShopFactories;
using StoreDefinition.Domain.ShopAggregateRoot.Errors;
using StoreDefinition.Domain.ShopAggregateRoot.ValueObjects;

namespace StoreDefinition.Application.Tests.Shops.QueryHandlers;
public class GetShopByIdQueryHandlerTests
{
    private readonly IShopRepository shopRepository;
    private readonly GetShopByIdQueryHandler _handler;

    public GetShopByIdQueryHandlerTests()
    {
        shopRepository = Substitute.For<IShopRepository>();
        _handler = new GetShopByIdQueryHandler(shopRepository);
    }

    [Fact]
    public async Task Handler_ReturnsShop_WhenShopExists()
    {
        var shop = ShopFactory.CreateValid();
        shopRepository.GetShopByIdAsync(shop.Id).Returns(shop);
        var query = new GetShopByIdQuery(shop.Id);

        var result = await _handler.Handle(query, default);

        result.IsSuccess.ShouldBeTrue();
        result.Value.ShouldBeEquivalentTo(shop);
        result.Errors.ShouldBeNull();
    }

    [Fact]
    public async Task Handler_ReturnsNotFoundByIdError_WhenShopNotExists()
    {
        shopRepository.GetShopByIdAsync(Arg.Any<ShopId>()).ReturnsNullForAnyArgs();
        var query = new GetShopByIdQuery(Guid.NewGuid());

        var result = await _handler.Handle(query, default);

        result.IsSuccess.ShouldBeFalse();
        result.Value.ShouldBeNull();
        result.Errors.ShouldNotBeNull();
        result.Errors.ShouldContain(ShopErrors.NotFoundById);
    }
}
