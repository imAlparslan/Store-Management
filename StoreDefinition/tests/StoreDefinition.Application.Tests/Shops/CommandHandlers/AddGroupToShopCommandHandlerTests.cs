using FluentAssertions;
using NSubstitute;
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

        result.IsSuccess.Should().BeTrue();
        result.Errors.Should().BeNullOrEmpty();
        result.Value!.HasGroup(groupId).Should().BeTrue();
        shop.GetDomainEvents().Should().HaveCount(1);
    }

    [Fact]
    public async Task Handler_ReturnsNotFoundByIdError_WhenGroupNotExists()
    {
        var groupId = Guid.NewGuid();
        _shopRepository.GetShopByIdAsync(Arg.Any<ShopId>()).ReturnsNullForAnyArgs();
        var command = new AddGroupToShopCommand(Guid.NewGuid(), groupId);

        var result = await _handler.Handle(command, default);

        result.IsSuccess.Should().BeFalse();
        result.Errors.Should().NotBeNullOrEmpty();
        result.Errors.Should().Contain([ShopErrors.NotFoundById]);
        result.Value.Should().BeNull();
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

        result.IsSuccess.Should().BeFalse();
        result.Errors.Should().NotBeNullOrEmpty();
        result.Errors.Should().Contain(ShopErrors.GroupNotAddedToShop);
        result.Value.Should().BeNull();
    }
}
