using FluentAssertions;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using StoreDefinition.Application.Common.Repositories;
using StoreDefinition.Application.Shops.Commands.DeleteShop;
using StoreDefinition.Application.Tests.Common.Factories.ShopFactories;
using StoreDefinition.Domain.ShopAggregateRoot.Errors;
using StoreDefinition.Domain.ShopAggregateRoot.ValueObjects;

namespace StoreDefinition.Application.Tests.Shops.CommandHandlers;
public class DeleteShopCommandHandlerTests
{
    private readonly IShopRepository shopRepository;
    private readonly DeleteShopCommandHandler _handler;

    public DeleteShopCommandHandlerTests()
    {
        shopRepository = Substitute.For<IShopRepository>();
        _handler = new DeleteShopCommandHandler(shopRepository);
    }

    [Fact]
    public async Task Handler_ReturnsTrue_WhenShopDeleted()
    {
        var shop = ShopFactory.CreateValid();
        var command = new DeleteShopCommand(shop.Id);
        shopRepository.GetShopByIdAsync(Arg.Any<ShopId>()).Returns(shop);
        shopRepository.DeleteShopByIdAsync(Arg.Any<ShopId>()).Returns(true);
        var result = await _handler.Handle(command, default);

        result.IsSuccess.Should().BeTrue();
        result.Value.Should().BeTrue();
        result.Errors.Should().BeNullOrEmpty();
        shop.GetDomainEvents().Should().HaveCount(1);
    }

    [Fact]
    public async Task Handler_ReturnsNotFoundByIdError_WhenIdNotExists()
    {
        var command = new DeleteShopCommand(Guid.NewGuid());
        shopRepository.GetShopByIdAsync(Arg.Any<ShopId>()).ReturnsNullForAnyArgs();
        var result = await _handler.Handle(command, default);

        result.IsSuccess.Should().BeFalse();
        result.Errors.Should().Contain(ShopErrors.NotFoundById);
    }
}
