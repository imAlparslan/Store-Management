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

        result.IsValid.ShouldBeTrue();
        result.Errors.ShouldBeEmpty();
    }

    [Theory]
    [ClassData(typeof(InvalidStrings))]
    public void Validator_ReturnsFalse_WhenDescriptionInvalid(string invalid)
    {
        var command = CreateShopCommandFactory.CreateCustom(description: invalid);
        var validator = new CreateShopCommandValidator();

        var result = validator.Validate(command);

        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldHaveSingleItem();
        result.Errors.Select(x => x.PropertyName)
            .ShouldContain(nameof(command.Description));
    }
    [Theory]
    [ClassData(typeof(InvalidStrings))]
    public void Validator_ReturnsFalse_WhenCityInvalid(string invalid)
    {
        var command = CreateShopCommandFactory.CreateCustom(city: invalid);
        var validator = new CreateShopCommandValidator();

        var result = validator.Validate(command);

        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldHaveSingleItem();
        result.Errors.Select(x => x.PropertyName
            ).ShouldContain(nameof(command.City));
    }

    [Theory]
    [ClassData(typeof(InvalidStrings))]
    public void Validator_ReturnsFalse_WhenStreetInvalid(string invalid)
    {
        var command = CreateShopCommandFactory.CreateCustom(street: invalid);
        var validator = new CreateShopCommandValidator();

        var result = validator.Validate(command);

        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldHaveSingleItem();
        result.Errors.Select(x => x.PropertyName)
            .ShouldContain(nameof(command.Street));
    }

    [Theory]
    [InlineData("", "", "")]
    public void Validator_ReturnsFalse_WhenDataInvalid(string description, string city, string street)
    {
        var command = CreateShopCommandFactory.CreateCustom(description, city, street);
        var validator = new CreateShopCommandValidator();

        var result = validator.Validate(command);

        result.IsValid.ShouldBeFalse();
        result.Errors.Count.ShouldBe(3);
        result.Errors.Select(x => x.PropertyName).ShouldBeSubsetOf([
                nameof(command.Description),
                nameof(command.City),
                nameof(command.Street)]);
    }
}