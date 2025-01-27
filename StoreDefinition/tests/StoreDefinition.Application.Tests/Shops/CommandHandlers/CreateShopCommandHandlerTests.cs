using FluentAssertions;
using FluentAssertions.Execution;
using NSubstitute;
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
    private readonly IGroupRepository _mockGroupRepository;
    public CreateShopCommandHandlerTests()
    {
        _mockShopRepository = Substitute.For<IShopRepository>();
        _mockGroupRepository = Substitute.For<IGroupRepository>();
        _handler = new CreateShopCommandHandler(_mockShopRepository, _mockGroupRepository);
    }

    [Fact]
    public async Task Handler_ReturnsInsertedShop_WhenDataValid()
    {
        var command = CreateShopCommandFactory.CreateValid();
        var shop = ShopFactory.CreateCustom(command.Description, command.City, command.Street);
        _mockShopRepository.InsertShopAsync(shop).ReturnsForAnyArgs(shop);

        var result = await _handler.Handle(command, default);

        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNull();
        result.Errors.Should().BeNullOrEmpty();
    }

    [Theory]
    [MemberData(nameof(invalidStrings))]
    public void Handler_ThrowsShopException_WhenDescriptionInvalid(string invalid)
    {
        var command = CreateShopCommandFactory.CreateCustom(description: invalid);
        _mockShopRepository.InsertShopAsync(Arg.Any<Shop>()).ReturnsForAnyArgs(ShopFactory.CreateValid());
        var result = () => _handler.Handle(command, default);

        using (AssertionScope scope = new())
        {
            result.Should().ThrowExactlyAsync<ShopException>();
        }
    }

    [Theory]
    [MemberData(nameof(invalidStrings))]
    public void Handler_ThrowsShopException_WhenCityInvalid(string invalid)
    {
        var command = CreateShopCommandFactory.CreateCustom(city: invalid);

        var result = () => _handler.Handle(command, default);

        using (AssertionScope scope = new())
        {
            result.Should().ThrowExactlyAsync<ShopException>();
        }
    }

    [Theory]
    [MemberData(nameof(invalidStrings))]
    public void Handler_ThrowsShopException_WhenStreetInvalid(string invalid)
    {
        var command = CreateShopCommandFactory.CreateCustom(street: invalid);

        var result = () => _handler.Handle(command, default);

        using (AssertionScope scope = new())
        {
            result.Should().ThrowExactlyAsync<ShopException>();
        }
    }


    public static readonly TheoryData<string> invalidStrings = ["", " ", null];

}
