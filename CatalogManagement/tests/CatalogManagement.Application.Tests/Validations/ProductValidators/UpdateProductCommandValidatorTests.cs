using CatalogManagement.Application.Products.Commands.UpdateProduct;
using CatalogManagement.Application.Tests.Common.Factories.CommandFactories;

namespace CatalogManagement.Application.Tests.Validations.ProductValidators;
public class UpdateProductCommandValidatorTests
{
    [Fact]
    public void Validator_ReturnsValidResult_WhenDataValid()
    {
        var command = UpdateProductCommandFactory.CreateValid();
        var validator = new UpdateProductCommandValidator();

        var result = validator.Validate(command);

        using (AssertionScope scope = new())
        {

            result.IsValid.Should().BeTrue();
            result.Errors.Should().BeEmpty();
        }
    }

    [Theory]
    [InlineData(default, "", "", "")]
    [InlineData(default, " ", " ", " ")]
    [InlineData(null, null, null, null)]
    public void Validator_ReturnsValidationError_WhenDataInvalid(Guid id, string productName, string productCode, string productDefinition)
    {
        var command = UpdateProductCommandFactory.CreateCustom(id, productName, productCode, productDefinition);
        var validator = new UpdateProductCommandValidator();

        var result = validator.Validate(command);

        using (AssertionScope scope = new())
        {
            result.IsValid.Should().BeFalse();
            result.Errors.Count.Should().Be(4);
            result.Errors.Select(x => x.PropertyName).Should()
                .Contain([nameof(command.Id), nameof(command.ProductName), nameof(command.ProductCode), nameof(command.ProductDefinition)]);
        }
    }



    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void Validator_ReturnsValidationError_WhenProductNameInvalid(string productName)
    {
        var command = UpdateProductCommandFactory.CreateWithName(productName);
        var validator = new UpdateProductCommandValidator();

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
    [MemberData(nameof(InvalidGuidData))]
    public void Validator_ReturnsValidationError_WhenProductIdInvalid(Guid productId)
    {
        var command = UpdateProductCommandFactory.CreateWithId(productId);
        var validator = new UpdateProductCommandValidator();

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
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void Validator_ReturnsValidationError_WhenProductCodeInvalid(string productCode)
    {
        var command = UpdateProductCommandFactory.CreateWithCode(productCode);
        var validator = new UpdateProductCommandValidator();

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
    public void Validator_ReturnsValidationError_WhenProductDefinitionInvalid(string productDefinition)
    {
        var command = UpdateProductCommandFactory.CreateWithDefinition(productDefinition);
        var validator = new UpdateProductCommandValidator();

        var result = validator.Validate(command);

        using (AssertionScope scope = new())
        {
            result.IsValid.Should().BeFalse();
            result.Errors.Count.Should().Be(1);
            result.Errors.Select(x => x.PropertyName).Should()
                .Contain([nameof(command.ProductDefinition)]);
        }
    }
    public static IEnumerable<object[]> InvalidGuidData => new List<object[]> {
        new object[] { null },
        new object[] { Guid.Empty },
        new object[] { default(Guid) }
    };
}
