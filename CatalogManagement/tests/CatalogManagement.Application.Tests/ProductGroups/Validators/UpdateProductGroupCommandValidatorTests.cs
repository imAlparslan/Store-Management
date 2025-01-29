using CatalogManagement.Application.ProductGroups.Commands.UpdateProductGroup;

namespace CatalogManagement.Application.Tests.ProductGroups.Validators;
public class UpdateProductGroupCommandValidatorTests
{
    private readonly UpdateProductGroupCommandValidator validator;
    public UpdateProductGroupCommandValidatorTests()
    {
        validator = new();
    }

    [Fact]
    public void Validator_ReturnsValidResult_WhenDataValid()
    {
        var command = UpdateProductGroupCommandFactory.CreateValid();

        var result = validator.Validate(command);

        using (AssertionScope scope = new())
        {

            result.IsValid.Should().BeTrue();
            result.Errors.Should().BeEmpty();
        }
    }

    [Theory]
    [InlineData(default, "", "")]
    [InlineData(default, " ", " ")]
    [InlineData(null, null, null)]
    public void Validator_ReturnsValidationError_WhenDataInvalid(Guid id, string group, string productGroupDescription)
    {
        var command = UpdateProductGroupCommandFactory.CreateCustom(id, group, productGroupDescription);

        var result = validator.Validate(command);

        using (AssertionScope scope = new())
        {
            result.IsValid.Should().BeFalse();
            result.Errors.Count.Should().Be(3);
            result.Errors.Select(x => x.PropertyName).Should()
                .Contain([nameof(command.Id), nameof(command.Name), nameof(command.Description)]);
        }
    }

    [Theory]
    [ClassData(typeof(InvalidStringData))]
    public void Validator_ReturnsValidationError_WhenProductGroupNameInvalid(string productGroupName)
    {
        var command = UpdateProductGroupCommandFactory.CreateWithName(productGroupName);

        var result = validator.Validate(command);

        using (AssertionScope scope = new())
        {
            result.IsValid.Should().BeFalse();
            result.Errors.Count.Should().Be(1);
            result.Errors.Select(x => x.PropertyName).Should()
                .Contain([nameof(command.Name)]);
        }
    }

    [Theory]
    [ClassData(typeof(InvalidGuidData))]
    public void Validator_ReturnsValidationError_WhenProductIdInvalid(Guid productGroupId)
    {
        var command = UpdateProductGroupCommandFactory.CreateWithId(productGroupId);

        var result = validator.Validate(command);

        using (AssertionScope scope = new())
        {
            result.IsValid.Should().BeFalse();
            result.Errors.Count.Should().Be(1);
            result.Errors.Select(x => x.PropertyName).Should()
                .Contain([nameof(command.Id)]);
        }
    }

    [Theory]
    [ClassData(typeof(InvalidStringData))]
    public void Validator_ReturnsValidationError_WhenProductGroupDescriptionInvalid(string productGroupDescription)
    {
        var command = UpdateProductGroupCommandFactory.CreateWithDefinition(productGroupDescription);

        var result = validator.Validate(command);

        using (AssertionScope scope = new())
        {
            result.IsValid.Should().BeFalse();
            result.Errors.Count.Should().Be(1);
            result.Errors.Select(x => x.PropertyName).Should()
                .Contain([nameof(command.Description)]);
        }
    }
}
