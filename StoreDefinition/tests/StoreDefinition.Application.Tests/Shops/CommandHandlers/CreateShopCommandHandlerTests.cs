using StoreDefinition.Application.Common.Repositories;
using StoreDefinition.Application.Shops.Commands.CreateShop;
using StoreDefinition.Application.Tests.Common.Factories.ShopFactories;
using StoreDefinition.Domain.ShopAggregateRoot;
using StoreDefinition.Domain.ShopAggregateRoot.Exceptions;

namespace StoreDefinition.Application.Tests.Shops.CommandHandlers;

public class CreateShopCommandHandlerTests
{
    private readonly CreateShopCommandHandler _handler;
    private readonly IShopRepository _mockShopRepository;
    public CreateShopCommandHandlerTests()
    {
        _mockShopRepository = Substitute.For<IShopRepository>();
        _handler = new CreateShopCommandHandler(_mockShopRepository);
    }

    [Fact]
    public async Task Handler_ReturnsInsertedShop_WhenDataValid()
    {
        var command = CreateShopCommandFactory.CreateValid();
        var shop = ShopFactory.CreateCustom(command.Description, command.City, command.Street);
        _mockShopRepository.InsertShopAsync(shop).ReturnsForAnyArgs(shop);

        var result = await _handler.Handle(command, default);

        result.IsSuccess.ShouldBeTrue();
        result.Value.ShouldNotBeNull();
        result.Errors.ShouldBeNull();
    }

    [Theory]
    [ClassData(typeof(InvalidStrings))]
    public void Handler_ThrowsShopException_WhenDescriptionInvalid(string invalid)
    {
        var command = CreateShopCommandFactory.CreateCustom(description: invalid);
        _mockShopRepository.InsertShopAsync(Arg.Any<Shop>()).ReturnsForAnyArgs(ShopFactory.CreateValid());
        var result = () => _handler.Handle(command, default);

        result.ShouldThrowAsync<ShopException>();
    }

    [Theory]
    [ClassData(typeof(InvalidStrings))]
    public void Handler_ThrowsShopException_WhenCityInvalid(string invalid)
    {
        var command = CreateShopCommandFactory.CreateCustom(city: invalid);

        var result = () => _handler.Handle(command, default);

        result.ShouldThrowAsync<ShopException>();
    }

    [Theory]
    [ClassData(typeof(InvalidStrings))]
    public void Handler_ThrowsShopException_WhenStreetInvalid(string invalid)
    {
        var command = CreateShopCommandFactory.CreateCustom(street: invalid);

        var result = () => _handler.Handle(command, default);

        result.ShouldThrowAsync<ShopException>();
    }
}
