using FluentAssertions;
using StoreDefinition.Application.Groups.Commands.AddShopToGroup;

namespace StoreDefinition.Application.Tests.Groups.Validations;
public class AddShopToGroupCommandValidatorTests
{
    private readonly AddShopToGroupCommandValidator validator;

    public AddShopToGroupCommandValidatorTests()
    {
        validator = new AddShopToGroupCommandValidator();
    }

    [Fact]
    public void Validator_ReturnsTrue_WhenCommandValid()
    {
        var command = new AddShopToGroupCommand(Guid.NewGuid(), Guid.NewGuid());

        var result = validator.Validate(command);

        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public void Validate_ReturnsFalse_WhenCommandShopIdInvalid()
    {
        var command = new AddShopToGroupCommand(Guid.NewGuid(), Guid.Empty);

        var result = validator.Validate(command);

        result.IsValid.Should().BeFalse();
        result.Errors.Select(x => x.PropertyName)
            .Should().Contain(nameof(command.ShopId));
    }

    [Fact]
    public void Validate_ReturnsFalse_WhenCommandGroupIdInvalid()
    {
        var command = new AddShopToGroupCommand(Guid.Empty, Guid.NewGuid());

        var result = validator.Validate(command);

        result.IsValid.Should().BeFalse();
        result.Errors.Select(x => x.PropertyName)
            .Should().Contain(nameof(command.GroupId));
    }

    [Fact]
    public void Validate_ReturnsFalse_WhenCommandInvalid()
    {
        var command = new AddShopToGroupCommand(Guid.Empty, Guid.Empty);

        var result = validator.Validate(command);

        result.IsValid.Should().BeFalse();
        result.Errors.Select(x => x.PropertyName)
            .Should().Contain([nameof(command.ShopId), nameof(command.GroupId)]);
    }
}