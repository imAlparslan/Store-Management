using CatalogManagement.Domain.ProductAggregate.Errors;
using CatalogManagement.Domain.ProductAggregate.Exceptions;
using CatalogManagement.Domain.ProductAggregate.ValueObjects;
using CatalogManagement.Domain.Tests.Common.InvalidData;
using CatalogManagement.Domain.Tests.ProductAggregate.Factories;

namespace CatalogManagement.Domain.Tests.ProductAggregate;

public class ProductValueObjectTests
{
    [Theory]
    [ClassData(typeof(InvalidStringData))]
    public void Creating_ProductName_Should_Throw_Exception_When_Argument_Null_OrWhiteSpace(string name)
    {
        var productName = () => new ProductName(name);

        productName.ShouldThrow<ProductException>()
            .ShouldSatisfyAllConditions(
                x => x.Message.ShouldBe(ProductError.InvalidName.Description),
                x => x.Code.ShouldBe(ProductError.InvalidName.Code),
                x => x.ShouldBeAssignableTo<DomainException>()
            );

    }

    [Theory]
    [ClassData(typeof(InvalidStringData))]
    public void Creating_ProductCode_Should_Throw_Exception_When_Argument_Null_OrWhiteSpace(string code)
    {
        var productCode = () => new ProductCode(code);

        productCode.ShouldThrow<ProductException>()
            .ShouldSatisfyAllConditions(
                x => x.Message.ShouldBe(ProductError.InvalidCode.Description),
                x => x.Code.ShouldBe(ProductError.InvalidCode.Code),
                x => x.ShouldBeAssignableTo<DomainException>()
            );
    }
    [Theory]
    [ClassData(typeof(InvalidStringData))]
    public void Creating_ProductDefinition_Should_Throw_Exception_When_Argument_Null_OrWhiteSpace(string definition)
    {
        var productDefinition = () => new ProductDefinition(definition);

        productDefinition.ShouldThrow<ProductException>()
            .ShouldSatisfyAllConditions(
                x => x.Message.ShouldBe(ProductError.InvalidDefinition.Description),
                x => x.Code.ShouldBe(ProductError.InvalidDefinition.Code),
                x => x.ShouldBeAssignableTo<DomainException>()
            );
    }

    [Fact]
    public void Product_Should_Have_ProductId_After_Creating()
    {
        var product = ProductFactory.Create();

        product.ShouldNotBeNull();
        product.Id.ShouldNotBeNull();
    }
}
