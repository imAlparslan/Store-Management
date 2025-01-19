using FluentAssertions;
using FluentAssertions.Execution;
using StoreDefinition.Application.Shops.Commands.UpdateShop;
using StoreDefinition.Application.Tests.Common.Factories.ShopFactories;

namespace StoreDefinition.Application.Tests.Shops.Validations;
public class UpdateShopCommandValidatorTests
{
    [Fact]
    public void Validator_ReturnsTrue_WhenCommandValid()
    {
        var command = UpdateShopCommandFactory.CreateValid();
        var validator = new UpdateShopCommandValidator();

        var result = validator.Validate(command);

        result.IsValid.Should().BeTrue();
    }

    [Theory]
    [MemberData(nameof(invalidStrings))]
    public void Validator_ReturnsFalse_WhenDescriptionInvalid(string invalid)
    {
        var command = UpdateShopCommandFactory.CreateCustom(description: invalid);
        var validator = new UpdateShopCommandValidator();

        var result = validator.Validate(command);

        using (AssertionScope scope = new())
        {
            result.IsValid.Should().BeFalse();
            result.Errors.Count.Should().Be(1);
            result.Errors.Select(x => x.PropertyName).Should()
                .Contain([nameof(command.Description)]);
        }
    }

    [Theory]
    [MemberData(nameof(invalidStrings))]
    public void Validator_ReturnsFalse_WhenCityInvalid(string invalid)
    {
        var command = UpdateShopCommandFactory.CreateCustom(city: invalid);
        var validator = new UpdateShopCommandValidator();

        var result = validator.Validate(command);

        using (AssertionScope scope = new())
        {
            result.IsValid.Should().BeFalse();
            result.Errors.Count.Should().Be(1);
            result.Errors.Select(x => x.PropertyName).Should()
                .Contain([nameof(command.City)]);
        }
    }

    [Theory]
    [MemberData(nameof(invalidStrings))]
    public void Validator_ReturnsFalse_WhenStreetInvalid(string invalid)
    {
        var command = UpdateShopCommandFactory.CreateCustom(street: invalid);
        var validator = new UpdateShopCommandValidator();

        var result = validator.Validate(command);

        using (AssertionScope scope = new())
        {
            result.IsValid.Should().BeFalse();
            result.Errors.Count.Should().Be(1);
            result.Errors.Select(x => x.PropertyName).Should()
                .Contain([nameof(command.Street)]);
        }
    }

    [Theory]
    [InlineData("00000000-0000-0000-0000-000000000000", "", "", "")]
    public void Validator_ReturnsFalse_WhenDataInvalid(Guid id, string description, string city, string street)
    {
        var command = UpdateShopCommandFactory.CreateCustom(description, city, street, id);
        var validator = new UpdateShopCommandValidator();

        var result = validator.Validate(command);

        using (AssertionScope scope = new())
        {
            result.IsValid.Should().BeFalse();
            result.Errors.Count.Should().Be(4);
            result.Errors.Select(x => x.PropertyName).Should()
                .Contain([
                    nameof(command.ShopId),
                    nameof(command.Description),
                    nameof(command.City),
                    nameof(command.Street)]);
        }
    }

    public static readonly TheoryData<string> invalidStrings = ["", " ", null];
}
