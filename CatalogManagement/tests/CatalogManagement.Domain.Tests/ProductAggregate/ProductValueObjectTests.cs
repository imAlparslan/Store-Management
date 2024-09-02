using CatalogManagement.Domain.ProductAggregate.ValueObjects;
using CatalogManagement.Domain.Tests.ProductAggregate.Factories;

namespace CatalogManagement.Domain.Tests.ProductAggregate;

public class ProductValueObjectTests
{
    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void Creating_ProductName_Should_Throw_Exception_When_Argument_Null_OrWhiteSpace(string name)
    {
        var productName = () => new ProductName(name);

        productName.Should().Throw<Exception>();

    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void Creating_ProductCode_Should_Throw_Exception_When_Argument_Null_OrWhiteSpace(string code)
    {
        var productCode = () => new ProductCode(code);

        productCode.Should().Throw<Exception>();

    }
    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void Creating_ProductDefinition_Should_Throw_Exception_When_Argument_Null_OrWhiteSpace(string definition)
    {
        var productDefinition = () => new ProductDefinition(definition);

        productDefinition.Should().Throw<Exception>();
    }

    [Fact]
    public void Product_Should_Have_ProductId_After_Creating()
    {
        var product = ProductFactory.Create();
        using (new AssertionScope())
        {
            product.Id.Should().NotBeNull();
            product.Id.Value.Should().NotBeEmpty();
        }
    }
}
