using CatalogManagement.Application.Products.Commands.CreateProduct;
using CatalogManagement.Application.Tests.Common.Factories.CommandFactories;
using FluentAssertions;
using FluentAssertions.Execution;

namespace CatalogManagement.Application.Tests.Validations.ProductValidators;
public class CreateProductCommandValidatorTests
{
    [Fact]
    public void Validator_ReturnsValidResult_WhenDataValid()
    {
        var command = CreateProductCommandFactory.CreateValid();
        var validator = new CreateProductCommandValidator();

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
        var validator = new CreateProductCommandValidator();

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
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void Validator_ReturnsInvalidResult_WhenProductNameInvalid(string productName)
    {
        var command = CreateProductCommandFactory.CreateWithName(productName);
        var validator = new CreateProductCommandValidator();

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
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void Validator_ReturnsInvalidResult_WhenProductCodeInvalid(string productCode)
    {
        var command = CreateProductCommandFactory.CreateWithCode(productCode);
        var validator = new CreateProductCommandValidator();

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
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void Validator_ReturnsInvalidResult_WhenProductDefinitionInvalid(string productDefinition)
    {
        var command = CreateProductCommandFactory.CreateWithDefinition(productDefinition);
        var validator = new CreateProductCommandValidator();

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
