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
        var command = new DeleteProductByIdCommand(id);

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
