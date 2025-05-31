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

        result.IsValid.ShouldBeTrue();
    }

    [Theory]
    [ClassData(typeof(InvalidStrings))]
    public void Validator_ReturnsFalse_WhenDescriptionInvalid(string invalid)
    {
        var command = UpdateShopCommandFactory.CreateCustom(description: invalid);
        var validator = new UpdateShopCommandValidator();

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
        var command = UpdateShopCommandFactory.CreateCustom(city: invalid);
        var validator = new UpdateShopCommandValidator();

        var result = validator.Validate(command);

        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldHaveSingleItem();
        result.Errors.Select(x => x.PropertyName)
            .ShouldContain(nameof(command.City));
    }

    [Theory]
    [ClassData(typeof(InvalidStrings))]
    public void Validator_ReturnsFalse_WhenStreetInvalid(string invalid)
    {
        var command = UpdateShopCommandFactory.CreateCustom(street: invalid);
        var validator = new UpdateShopCommandValidator();

        var result = validator.Validate(command);

        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldHaveSingleItem();
        result.Errors.Select(x => x.PropertyName)
            .ShouldContain(nameof(command.Street));
    }

    [Theory]
    [InlineData("00000000-0000-0000-0000-000000000000", "", "", "")]
    public void Validator_ReturnsFalse_WhenDataInvalid(Guid id, string description, string city, string street)
    {
        var command = UpdateShopCommandFactory.CreateCustom(description, city, street, id);
        var validator = new UpdateShopCommandValidator();

        var result = validator.Validate(command);

        result.IsValid.ShouldBeFalse();
        result.Errors.Count.ShouldBe(4);
        result.Errors.Select(x => x.PropertyName)
            .ShouldBeSubsetOf([
                nameof(command.ShopId),
                    nameof(command.Description),
                    nameof(command.City),
                    nameof(command.Street)]);
    }
}
