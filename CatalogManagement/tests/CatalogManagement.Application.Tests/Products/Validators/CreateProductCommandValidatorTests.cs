using CatalogManagement.Application.Products.Commands.CreateProduct;

namespace CatalogManagement.Application.Tests.Products.Validators;
public class CreateProductCommandValidatorTests
{
    private readonly CreateProductCommandValidator validator;
    public CreateProductCommandValidatorTests()
    {
        validator = new();
    }

    [Fact]
    public void Validator_ReturnsValidResult_WhenDataValid()
    {
        var command = CreateProductCommandFactory.CreateValid();

        var result = validator.Validate(command);

        using (AssertionScope scope = new())
        {
            result.IsValid.Should().BeTrue();
            result.Errors.Should().BeEmpty();
        }
    }

    [Theory]
    [InlineData("", "", "")]
    [InlineData(" ", " ", " ")]
    [InlineData(null, null, null)]
    public void Validator_ReturnsInvalidResult_WhenDataInvalid(string productName, string productCode, string productDefinition)
    {
        var command = CreateProductCommandFactory.CreateCustom(productName, productCode, productDefinition);

        var result = validator.Validate(command);

        using (AssertionScope scope = new())
        {
            result.IsValid.Should().BeFalse();
            result.Errors.Count.Should().Be(3);
            result.Errors.Select(x => x.PropertyName).Should()
                .Contain([nameof(command.ProductName), nameof(command.ProductCode), nameof(command.ProductDefinition)]);
        }
    }


    [Theory]
    [ClassData(typeof(InvalidStringData))]
    public void Validator_ReturnsInvalidResult_WhenProductNameInvalid(string productName)
    {
        var command = CreateProductCommandFactory.CreateWithName(productName);

        var result = validator.Validate(command);

        using (AssertionScope scope = new())
        {
            result.IsValid.Should().BeFalse();
            result.Errors.Count.Should().Be(1);
            result.Errors.Select(x => x.PropertyName).Should()
                .Contain([nameof(command.ProductName)]);
        }
    }
    [Theory]
    [ClassData(typeof(InvalidStringData))]
    public void Validator_ReturnsInvalidResult_WhenProductCodeInvalid(string productCode)
    {
        var command = CreateProductCommandFactory.CreateWithCode(productCode);

        var result = validator.Validate(command);

        using (AssertionScope scope = new())
        {
            result.IsValid.Should().BeFalse();
            result.Errors.Count.Should().Be(1);
            result.Errors.Select(x => x.PropertyName).Should()
                .Contain([nameof(command.ProductCode)]);
        }
    }

    [Theory]
    [ClassData(typeof(InvalidStringData))]
    public void Validator_ReturnsInvalidResult_WhenProductDefinitionInvalid(string productDefinition)
    {
        var command = CreateProductCommandFactory.CreateWithDefinition(productDefinition);

        var result = validator.Validate(command);

        using (AssertionScope scope = new())
        {
            result.IsValid.Should().BeFalse();
            result.Errors.Count.Should().Be(1);
            result.Errors.Select(x => x.PropertyName).Should()
                .Contain([nameof(command.ProductDefinition)]);
        }
    }
}
