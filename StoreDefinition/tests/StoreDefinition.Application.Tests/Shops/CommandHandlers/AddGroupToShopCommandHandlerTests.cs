using NSubstitute.ReturnsExtensions;
using StoreDefinition.Application.Common.Repositories;
using StoreDefinition.Application.Shops.Commands.AddGroupToShop;
using StoreDefinition.Application.Tests.Common.Factories.ShopFactories;
using StoreDefinition.Domain.ShopAggregateRoot;
using StoreDefinition.Domain.ShopAggregateRoot.Errors;
using StoreDefinition.Domain.ShopAggregateRoot.ValueObjects;

namespace StoreDefinition.Application.Tests.Shops.CommandHandlers;
public class AddGroupToShopCommandHandlerTests
{
    private readonly IShopRepository _shopRepository;
    private readonly AddGroupToShopCommandHandler _handler;
    public AddGroupToShopCommandHandlerTests()
    {
        _shopRepository = Substitute.For<IShopRepository>();
        _handler = new AddGroupToShopCommandHandler(_shopRepository);
    }

    [Fact]
    public async Task Handler_ReturnsShop_WhenGroupAdded()
    {
        var groupId = Guid.NewGuid();
        var shop = ShopFactory.CreateValid();
        _shopRepository.GetShopByIdAsync(Arg.Any<ShopId>()).ReturnsForAnyArgs(shop);
        _shopRepository.UpdateShopAsync(Arg.Any<Shop>()).ReturnsForAnyArgs(shop);
        var command = new AddGroupToShopCommand(shop.Id, groupId);

        var result = await _handler.Handle(command, default);

        result.IsSuccess.ShouldBeTrue();
        result.Errors.ShouldBeNull();
        result.Value.ShouldNotBeNull();
        result.Value.HasGroup(groupId).ShouldBeTrue();
        shop.GetDomainEvents().ShouldHaveSingleItem();
    }

    [Fact]
    public async Task Handler_ReturnsNotFoundByIdError_WhenGroupNotExists()
    {
        var groupId = Guid.NewGuid();
        _shopRepository.GetShopByIdAsync(Arg.Any<ShopId>()).ReturnsNullForAnyArgs();
        var command = new AddGroupToShopCommand(Guid.NewGuid(), groupId);

        var result = await _handler.Handle(command, default);

        result.IsSuccess.ShouldBeFalse();
        result.Errors.ShouldNotBeNull();
        result.Errors.ShouldContain(ShopErrors.NotFoundById);
        result.Value.ShouldBeNull();
    }

    [Fact]
    public async Task Handler_ReturnsGroupNotAddedToShopError_WhenGroupNotAdded()
    {
        var groupId = Guid.NewGuid();
        var shop = ShopFactory.CreateValid();
        shop.AddGroup(groupId);
        _shopRepository.GetShopByIdAsync(Arg.Any<ShopId>()).ReturnsForAnyArgs(shop);
        var command = new AddGroupToShopCommand(Guid.NewGuid(), groupId);

        var result = await _handler.Handle(command, default);

        result.IsSuccess.ShouldBeFalse();
        result.Errors.ShouldNotBeNull();
        result.Errors.ShouldContain(ShopErrors.GroupNotAddedToShop);
        result.Value.ShouldBeNull();
    }
}
