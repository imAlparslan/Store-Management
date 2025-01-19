using FluentAssertions;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using StoreDefinition.Application.Common.Repositories;
using StoreDefinition.Application.Shops.Commands.RemoveGroupFromShop;
using StoreDefinition.Application.Tests.Common.Factories.ShopFactories;
using StoreDefinition.Domain.ShopAggregateRoot;
using StoreDefinition.Domain.ShopAggregateRoot.Errors;
using StoreDefinition.Domain.ShopAggregateRoot.ValueObjects;

namespace StoreDefinition.Application.Tests.Shops.CommandHandlers;
public class RemoveGroupFromShopCommandHandlerTests
{
    private readonly IShopRepository _shopRepository;
    private readonly RemoveGroupFromShopCommandHandler _handler;
    public RemoveGroupFromShopCommandHandlerTests()
    {
        _shopRepository = Substitute.For<IShopRepository>();
        _handler = new RemoveGroupFromShopCommandHandler(_shopRepository);
    }

    [Fact]
    public async Task Handler_ReturnsShop_WhenGroupRemoved()
    {
        var groupId = Guid.NewGuid();
        var shop = ShopFactory.CreateValid();
        shop.AddGroup(groupId);
        _shopRepository.GetShopByIdAsync(Arg.Any<ShopId>()).ReturnsForAnyArgs(shop);
        _shopRepository.UpdateShopAsync(Arg.Any<Shop>()).ReturnsForAnyArgs(shop);
        var command = new RemoveGroupFromShopCommand(shop.Id, groupId);

        var result = await _handler.Handle(command, default);

        result.IsSuccess.Should().BeTrue();
        result.Value!.HasGroup(groupId).Should().BeFalse();
        result.Value!.GetDomainEvents().Should().HaveCount(1);
        result.Errors.Should().BeNullOrEmpty();
    }
    [Fact]
    public async Task Handler_ReturnsNotFoundById_WhenShopNotExists()
    {
        _shopRepository.GetShopByIdAsync(Arg.Any<ShopId>()).ReturnsNull();
        var command = new RemoveGroupFromShopCommand(Guid.NewGuid(), Guid.NewGuid());

        var result = await _handler.Handle(command, default);

        result.IsSuccess.Should().BeFalse();
        result.Errors.Should().NotBeNullOrEmpty();
        result.Errors.Should().Contain(ShopErrors.NotFoundById);
    }
    [Fact]
    public async Task Handler_ReturnsGroupNotRemovedFromShop_WhenGroupNotRemoved()
    {
        var shop = ShopFactory.CreateValid();
        _shopRepository.GetShopByIdAsync(Arg.Any<ShopId>()).ReturnsForAnyArgs(shop);
        var command = new RemoveGroupFromShopCommand(Guid.NewGuid(), Guid.NewGuid());

        var result = await _handler.Handle(command, default);

        result.IsSuccess.Should().BeFalse();
        result.Errors.Should().NotBeNullOrEmpty();
        result.Errors.Should().Contain(ShopErrors.GroupNotRemovedFromShop);
    }
}
