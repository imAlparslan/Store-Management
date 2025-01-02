using CatalogManagement.Application.ProductGroups.Commands.AddProduct;

namespace CatalogManagement.Application.Tests.ProductGroups.Validators;
public class AddProductToGroupCommandValidatorTests
{

    [Fact]
    public void Validate_ShouldReturnTrue_WhenDataIsValid()
    {
        var command = new AddProductToGroupCommand(Guid.NewGuid(), Guid.NewGuid());
        var validator = new AddProductToGroupCommandValidator();

        var result = validator.Validate(command);

        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public void Validate_ShouldReturnFalse_WhenProductGroupIdIsEmpty()
    {
        var command = new AddProductToGroupCommand(Guid.Empty, Guid.NewGuid());
        var validator = new AddProductToGroupCommandValidator();

        var result = validator.Validate(command);

        result.IsValid.Should().BeFalse();
        result.Errors.Select(x => x.PropertyName).Should().Contain(nameof(command.ProductGroupId));
    }

    [Fact]
    public void Validate_ShouldReturnFalse_WhenProductIdIsEmpty()
    {
        var command = new AddProductToGroupCommand(Guid.NewGuid(), Guid.Empty);
        var validator = new AddProductToGroupCommandValidator();

        var result = validator.Validate(command);

        result.IsValid.Should().BeFalse();
        result.Errors.Select(x => x.PropertyName).Should().Contain(nameof(command.ProductId));
    }

    [Fact]
    public void Validate_ShouldReturnFalse_WhenProductGroupIdAndProductIdAreEmpty()
    {
        var command = new AddProductToGroupCommand(Guid.Empty, Guid.Empty);
        var validator = new AddProductToGroupCommandValidator();

        var result = validator.Validate(command);

        result.IsValid.Should().BeFalse();
        result.Errors.Select(x => x.PropertyName).Should().Contain(nameof(command.ProductGroupId));
        result.Errors.Select(x => x.PropertyName).Should().Contain(nameof(command.ProductId));
    }
}
