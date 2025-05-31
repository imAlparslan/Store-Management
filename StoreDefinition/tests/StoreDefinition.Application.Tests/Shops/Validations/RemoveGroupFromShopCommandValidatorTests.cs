using StoreDefinition.Application.Shops.Commands.RemoveGroupFromShop;

namespace StoreDefinition.Application.Tests.Shops.Validations;
public class RemoveGroupFromShopCommandValidatorTests
{
    [Fact]
    public void Validator_ReturnsTrue_WhenCommandValid()
    {
        var command = new RemoveGroupFromShopCommand(Guid.NewGuid(), Guid.NewGuid());
        var validator = new RemoveGroupFromShopCommandValidator();

        var result = validator.Validate(command);

        result.IsValid.ShouldBeTrue();
    }

    [Fact]
    public void Validator_ReturnsFalse_WhenShopIdInvalid()
    {
        var command = new RemoveGroupFromShopCommand(Guid.Empty, Guid.NewGuid());
        var validator = new RemoveGroupFromShopCommandValidator();

        var result = validator.Validate(command);

        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldNotBeNull();
        result.Errors.ShouldHaveSingleItem();
        result.Errors.Select(x => x.PropertyName)
            .ShouldContain(nameof(command.ShopId));
    }
    [Fact]
    public void Validator_ReturnsFalse_WhenGroupIdInvalid()
    {
        var command = new RemoveGroupFromShopCommand(Guid.NewGuid(), Guid.Empty);
        var validator = new RemoveGroupFromShopCommandValidator();

        var result = validator.Validate(command);

        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldNotBeNull();
        result.Errors.ShouldHaveSingleItem();
        result.Errors.Select(x => x.PropertyName)
            .ShouldContain(nameof(command.GroupId));
    }

    [Fact]
    public void Validator_ReturnsFalse_WhenCommandInvalid()
    {
        var command = new RemoveGroupFromShopCommand(Guid.Empty, Guid.Empty);
        var validator = new RemoveGroupFromShopCommandValidator();

        var result = validator.Validate(command);

        result.IsValid!.ShouldBeFalse();
        result.Errors.ShouldNotBeNull();
        result.Errors.Count.ShouldBe(2);
        result.Errors.Select(x => x.PropertyName)
            .ShouldBeSubsetOf([nameof(command.ShopId), nameof(command.GroupId)]);
    }

}
