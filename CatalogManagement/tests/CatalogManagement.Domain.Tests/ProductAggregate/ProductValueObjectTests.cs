using CatalogManagement.Domain.ProductAggregate.Errors;
using CatalogManagement.Domain.ProductAggregate.Exceptions;
using CatalogManagement.Domain.ProductAggregate.ValueObjects;
using CatalogManagement.Domain.Tests.ProductAggregate.Factories;

namespace CatalogManagement.Domain.Tests.ProductAggregate;

public class ProductValueObjectTests
{
    [Theory]
    [MemberData(nameof(invalidStrings))]
    public void Creating_ProductName_Should_Throw_Exception_When_Argument_Null_OrWhiteSpace(string name)
    {
        var productName = () => new ProductName(name);

        productName.Should().ThrowExactly<ProductException>()
            .Which.Should().BeAssignableTo<DomainException>()
            .Which.Code.Should().Be(ProductError.InvalidName.Code);

    }

    [Theory]
    [MemberData(nameof(invalidStrings))]
    public void Creating_ProductCode_Should_Throw_Exception_When_Argument_Null_OrWhiteSpace(string code)
    {
        var productCode = () => new ProductCode(code);

        productCode.Should().ThrowExactly<ProductException>()
            .Which.Should().BeAssignableTo<DomainException>()
            .Which.Code.Should().Be(ProductError.InvalidCode.Code);

    }
    [Theory]
    [MemberData(nameof(invalidStrings))]
    public void Creating_ProductDefinition_Should_Throw_Exception_When_Argument_Null_OrWhiteSpace(string definition)
    {
        var productDefinition = () => new ProductDefinition(definition);

        productDefinition.Should().ThrowExactly<ProductException>()
            .Which.Should().BeAssignableTo<DomainException>()
            .Which.Code.Should().Be(ProductError.InvalidDefinition.Code);
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
    public static readonly TheoryData<string> invalidStrings = ["", " ", null];

}
