using StoreDefinition.Application.Shops.Commands.AddGroupToShop;

namespace StoreDefinition.Application.Tests.Shops.Validations;
public class AddGroupToShopCommandValidatorTests
{

    [Fact]
    public void Validator_ReturnsValid_WhenCommandValid()
    {
        var command = new AddGroupToShopCommand(Guid.NewGuid(), Guid.NewGuid());
        var validator = new AddGroupToShopCommandValidator();

        var result = validator.Validate(command);

        result.IsValid.ShouldBeTrue();
    }


    [Fact]
    public void Validator_ReturnsFail_WhenCommandShopIdInvalid()
    {
        var command = new AddGroupToShopCommand(Guid.Empty, Guid.NewGuid());
        var validator = new AddGroupToShopCommandValidator();

        var result = validator.Validate(command);

        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldHaveSingleItem();
        result.Errors.Select(x => x.PropertyName)
            .ShouldContain(nameof(command.ShopId));
    }


    [Fact]
    public void Validator_ReturnsFail_WhenCommandGroupIdInvalid()
    {
        var command = new AddGroupToShopCommand(Guid.NewGuid(), Guid.Empty);
        var validator = new AddGroupToShopCommandValidator();

        var result = validator.Validate(command);

        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldHaveSingleItem();
        result.Errors.Select(x => x.PropertyName)
            .ShouldContain(nameof(command.GroupId));
    }

    [Fact]
    public void Validator_ReturnsFail_WhenCommandInvalid()
    {
        var command = new AddGroupToShopCommand(Guid.Empty, Guid.Empty);
        var validator = new AddGroupToShopCommandValidator();

        var result = validator.Validate(command);

        result.IsValid.ShouldBeFalse();
        result.Errors.Count.ShouldBe(2);
        result.Errors.Select(x => x.PropertyName)
            .ShouldBeSubsetOf([nameof(command.GroupId), nameof(command.ShopId)]);
    }
}
