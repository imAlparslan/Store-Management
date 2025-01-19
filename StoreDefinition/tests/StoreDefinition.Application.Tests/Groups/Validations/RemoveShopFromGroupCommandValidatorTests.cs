using FluentAssertions;
using StoreDefinition.Application.Groups.Commands.RemoveShopFromGroup;

namespace StoreDefinition.Application.Tests.Groups.Validations;
public class RemoveShopFromGroupCommandValidatorTests
{
    private readonly RemoveShopFromGroupCommandValidator validator;
    public RemoveShopFromGroupCommandValidatorTests()
    {
        validator = new RemoveShopFromGroupCommandValidator();
    }

    [Fact]
    public void Validator_ReturnsTrue_WhenCommandValid()
    {
        var command = new RemoveShopFromGroupCommand(Guid.NewGuid(), Guid.NewGuid());

        var result = validator.Validate(command);

        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public void Validator_ReturnsFalse_WhenGroupIdInvalid()
    {
        var command = new RemoveShopFromGroupCommand(Guid.Empty,Guid.NewGuid());

        var result = validator.Validate(command);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().HaveCount(1);
        result.Errors.Select(x => x.PropertyName)
            .Should().Contain(nameof(command.GroupId));
    }

    [Fact]
    public void Validator_ReturnsFalse_WhenShopIdInvalid()
    {
        var command = new RemoveShopFromGroupCommand(Guid.NewGuid(), Guid.Empty);

        var result = validator.Validate(command);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().HaveCount(1);
        result.Errors.Select(x => x.PropertyName)
            .Should().Contain(nameof(command.ShopId));
    }

    [Fact]
    public void Validator_ReturnsFalse_WhenCommandInvalid()
    {
        var command = new RemoveShopFromGroupCommand(Guid.Empty, Guid.Empty);

        var result = validator.Validate(command);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().HaveCount(2);
        result.Errors.Select(x => x.PropertyName)
            .Should().Contain([nameof(command.ShopId), nameof(command.GroupId)]);
    }
}
