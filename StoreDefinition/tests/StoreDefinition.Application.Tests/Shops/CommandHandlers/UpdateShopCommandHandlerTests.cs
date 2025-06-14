﻿using NSubstitute.ReturnsExtensions;
using StoreDefinition.Application.Common.Repositories;
using StoreDefinition.Application.Shops.Commands.UpdateShop;
using StoreDefinition.Application.Tests.Common.Factories.ShopFactories;
using StoreDefinition.Domain.ShopAggregateRoot;
using StoreDefinition.Domain.ShopAggregateRoot.Errors;
using StoreDefinition.Domain.ShopAggregateRoot.ValueObjects;

namespace StoreDefinition.Application.Tests.Shops.CommandHandlers;

public class UpdateShopCommandHandlerTests
{
    private readonly IShopRepository _shopRepository;
    private readonly UpdateShopCommandHandler _handler;
    public UpdateShopCommandHandlerTests()
    {
        _shopRepository = Substitute.For<IShopRepository>();
        _handler = new UpdateShopCommandHandler(_shopRepository);

    }

    [Fact]
    public async Task Handler_ReturnsUpdatedShop_WhenShopUpdated()
    {
        var shop = ShopFactory.CreateValid();
        var command = UpdateShopCommandFactory.CreateCustom(shopId: shop.Id);
        _shopRepository.GetShopByIdAsync(Arg.Any<ShopId>()).Returns(shop);
        _shopRepository.UpdateShopAsync(Arg.Any<Shop>()).Returns(shop);

        var result = await _handler.Handle(command, default);

        result.IsSuccess.ShouldBeTrue();
        result.Errors.ShouldBeNull();
        result.Value.ShouldNotBeNull();
        result.Value!.Description.Value.ShouldBe(command.Description);
        result.Value!.Address.City.ShouldBe(command.City);
        result.Value!.Address.Street.ShouldBe(command.Street);

    }

    [Fact]
    public async Task Handler_ReturnsNotFoundByIdError_WhenShopNotExists()
    {
        var command = UpdateShopCommandFactory.CreateValid();
        _shopRepository.GetShopByIdAsync(Arg.Any<ShopId>()).ReturnsNullForAnyArgs();

        var result = await _handler.Handle(command, default);

        result.IsSuccess!.ShouldBeFalse();
        result.Errors.ShouldNotBeNull();
        result.Errors.ShouldContain(ShopErrors.NotFoundById);
        result.Value.ShouldBeNull();
    }
}