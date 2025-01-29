using CatalogManagement.Application.ProductGroups.Commands.CreateProductGroup;

namespace CatalogManagement.Application.Tests.ProductGroups.Validators;
public class CreateProductGroupCommandValidatorTests
{
    private readonly CreateProductGroupCommandValidator validator;
    public CreateProductGroupCommandValidatorTests()
    {
        validator = new();
    }
    [Fact]
    public void Validator_ReturnsValidResult_WhenDataValid()
    {
        var command = CreateProductGroupCommandFactory.CreateValid();

        var result = validator.Validate(command);

        using (AssertionScope scope = new())
        {
            result.IsValid.Should().BeTrue();
            result.Errors.Should().BeEmpty();
        }
    }

    [Theory]
    [InlineData("", "")]
    [InlineData(" ", " ")]
    [InlineData(null, null)]
    public void Validator_ReturnsInvalidResult_WhenDataInvalid(string productName, string productGroupDescription)
    {
        var command = CreateProductGroupCommandFactory.CreateCustom(productName, productGroupDescription);

        var result = validator.Validate(command);

        using (AssertionScope scope = new())
        {
            result.IsValid.Should().BeFalse();
            result.Errors.Count.Should().Be(2);
            result.Errors.Select(x => x.PropertyName).Should()
                .Contain([nameof(command.Name), nameof(command.Description)]);
        }
    }


    [Theory]
    [ClassData(typeof(InvalidStringData))]
    public void Validator_ReturnsInvalidResult_WhenProductNameInvalid(string productGroup)
    {
        var command = CreateProductGroupCommandFactory.CreateWithName(productGroup);

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
    [ClassData(typeof(InvalidStringData))]
    public void Validator_ReturnsInvalidResult_WhenProductGroupDescriptionInvalid(string productGroupDescription)
    {
        var command = CreateProductGroupCommandFactory.CreateWithDefinition(productGroupDescription);

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
