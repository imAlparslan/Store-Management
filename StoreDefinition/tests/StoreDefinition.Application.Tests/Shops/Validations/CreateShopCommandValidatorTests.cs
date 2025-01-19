using FluentAssertions;
using FluentAssertions.Execution;
using StoreDefinition.Application.Shops.Commands.CreateShop;
using StoreDefinition.Application.Tests.Common.Factories.ShopFactories;

namespace StoreDefinition.Application.Tests.Shops.Validations;
public class CreateShopCommandValidatorTests
{

    [Fact]
    public void Validator_ReturnsTrue_WhenCommandIsValid()
    {
        var command = CreateShopCommandFactory.CreateValid();
        var validator = new CreateShopCommandValidator();

        var result = validator.Validate(command);

        result.IsValid.Should().BeTrue();
        result.Errors.Should().BeNullOrEmpty();
    }

    [Theory]
    [MemberData(nameof(invalidStrings))]
    public void Validator_ReturnsFalse_WhenDescriptionInvalid(string invalid)
    {
        var command = CreateShopCommandFactory.CreateCustom(description: invalid);
        var validator = new CreateShopCommandValidator();

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
        var command = CreateShopCommandFactory.CreateCustom(city: invalid);
        var validator = new CreateShopCommandValidator();

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
        var command = CreateShopCommandFactory.CreateCustom(street: invalid);
        var validator = new CreateShopCommandValidator();

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
    [InlineData("", "", "")]
    public void Validator_ReturnsFalse_WhenDataInvalid(string description, string city, string street)
    {
        var command = CreateShopCommandFactory.CreateCustom(description, city, street);
        var validator = new CreateShopCommandValidator();

        var result = validator.Validate(command);

        using (AssertionScope scope = new())
        {
            result.IsValid.Should().BeFalse();
            result.Errors.Count.Should().Be(3);
            result.Errors.Select(x => x.PropertyName).Should()
                .Contain([
                    nameof(command.Description),
                    nameof(command.City),
                    nameof(command.Street)]);
        }
    }
    public static readonly TheoryData<string> invalidStrings = ["", " ", null];

}
