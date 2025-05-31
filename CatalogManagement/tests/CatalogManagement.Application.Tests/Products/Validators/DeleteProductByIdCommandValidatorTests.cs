using CatalogManagement.Application.Products.Commands.DeleteProductById;

namespace CatalogManagement.Application.Tests.Products.Validators;

public class DeleteProductByIdCommandValidatorTests
{
    private readonly DeleteProductByIdCommandValidator validator;
    public DeleteProductByIdCommandValidatorTests()
    {
        validator = new();
    }

    [Fact]
    public void Validator_ReturnsSuccess_WhenIdValid()
    {
        var command = new DeleteProductByIdCommand(Guid.NewGuid());

        var result = validator.Validate(command);

        result.IsValid.ShouldBeTrue();
        result.Errors.ShouldBeEmpty();
    }

    [Theory]
    [ClassData(typeof(InvalidGuidData))]
    public void Validator_ReturnsValidationError_WhenProductIdInvalid(Guid id)
    {
        var command = new DeleteProductByIdCommand(id);

        var result = validator.Validate(command);

        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldHaveSingleItem();
        result.Errors.Select(x => x.PropertyName)
            .ShouldHaveSingleItem(nameof(command.Id));
    }
}
