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

        using (AssertionScope scope = new())
        {
            result.IsValid.Should().BeTrue();
            result.Errors.Should().BeEmpty();
        }
    }

    [Theory]
    [ClassData(typeof(InvalidGuidData))]
    public void Validator_ReturnsValidationError_WhenProductIdInvalid(Guid id)
    {
        var command = new DeleteProductGroupByIdCommand(id);

        var result = validator.Validate(command);

        using (AssertionScope scope = new())
        {
            result.IsValid.Should().BeFalse();
            result.Errors.Count.Should().Be(1);
            result.Errors.Select(x => x.PropertyName)
                .Should()
                .ContainSingle(nameof(command.Id));
        }
    }
}
