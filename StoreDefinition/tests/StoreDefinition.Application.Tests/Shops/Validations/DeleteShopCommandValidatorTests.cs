using FluentAssertions;
using StoreDefinition.Application.Shops.Commands.DeleteShop;

namespace StoreDefinition.Application.Tests.Shops.Validations;
public class DeleteShopCommandValidatorTests
{

    [Fact]
    public void Validator_ReturnsTrue_WhenCommandValid()
    {
        var command = new DeleteShopCommand(Guid.NewGuid());
        var validator = new DeleteShopCommandValidator();

        var result = validator.Validate(command);

        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public void Validator_ReturnsFalse_WhenCommandInvalid()
    {
        var command = new DeleteShopCommand(Guid.Empty);
        var validator = new DeleteShopCommandValidator();

        var result = validator.Validate(command);

        result.IsValid.Should().BeFalse();
        result.Errors.Select(x => x.PropertyName)
            .Should().Contain(nameof(command.ShopId));
    }
}
