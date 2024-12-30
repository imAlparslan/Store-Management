using CatalogManagement.Application.ProductGroups.Commands.RemoveProductFromProductGroup;

namespace CatalogManagement.Application.Tests.ProductGroups.Validators;
public class RemoveProductFromProductGroupCommandValidatorTests
{

    [Fact]
    public void Validate_ShouldReturnTrue_WhenDataIsValid()
    {
        var command = new RemoveProductFromProductGroupCommand(Guid.NewGuid(), Guid.NewGuid());
        var validator = new RemoveProductFromProductGroupCommandValidator();
        var result = validator.Validate(command);
        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public void Validate_ShouldReturnFalse_WhenProductGroupIdIsEmpty()
    {
        var command = new RemoveProductFromProductGroupCommand(Guid.Empty, Guid.NewGuid());
        var validator = new RemoveProductFromProductGroupCommandValidator();
        var result = validator.Validate(command);
        result.IsValid.Should().BeFalse();
        result.Errors.Select(x => x.PropertyName).Should().Contain(nameof(command.ProductGroupId));
    }

    [Fact]
    public void Validate_ShouldReturnFalse_WhenProductIdIsEmpty()
    {
        var command = new RemoveProductFromProductGroupCommand(Guid.NewGuid(), Guid.Empty);
        var validator = new RemoveProductFromProductGroupCommandValidator();
        var result = validator.Validate(command);
        result.IsValid.Should().BeFalse();
        result.Errors.Select(x => x.PropertyName).Should().Contain(nameof(command.ProductId));
    }
    [Fact]
    public void Validate_ShouldReturnFalse_WhenProductGroupIdAndProductIdAreEmpty()
    {
        var command = new RemoveProductFromProductGroupCommand(Guid.Empty, Guid.Empty);
        var validator = new RemoveProductFromProductGroupCommandValidator();
        var result = validator.Validate(command);
        result.IsValid.Should().BeFalse();
        result.Errors.Select(x => x.PropertyName).Should().Contain(nameof(command.ProductGroupId));
        result.Errors.Select(x => x.PropertyName).Should().Contain(nameof(command.ProductId));
    }
}
