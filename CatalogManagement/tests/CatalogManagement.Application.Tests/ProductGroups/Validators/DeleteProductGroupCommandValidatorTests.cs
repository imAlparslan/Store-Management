using CatalogManagement.Application.ProductGroups.Commands.DeleteProductGroup;

namespace CatalogManagement.Application.Tests.ProductGroups.Validators;

public class DeleteProductGroupCommandValidatorTests
{
    private readonly DeleteProductGroupByIdCommandValidator validator;
    public DeleteProductGroupCommandValidatorTests()
    {
        validator = new();
    }

    [Fact]
    public void Validator_ReturnsSuccess_WhenIdValid()
    {
        var command = new DeleteProductGroupByIdCommand(Guid.NewGuid());

        var result = validator.Validate(command);

        result.ShouldSatisfyAllConditions(
            x => result.IsValid.ShouldBeTrue(),
            x => result.Errors.ShouldBeEmpty()
        );
    }

    [Theory]
    [ClassData(typeof(InvalidGuidData))]
    public void Validator_ReturnsValidationError_WhenProductIdInvalid(Guid id)
    {
        var command = new DeleteProductGroupByIdCommand(id);

        var result = validator.Validate(command);

        result.ShouldSatisfyAllConditions(
            x => result.IsValid.ShouldBeFalse(),
            x => result.Errors.Select(x => x.PropertyName)
                .ShouldHaveSingleItem(nameof(command.Id))
        );
    }
}
