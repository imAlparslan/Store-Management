using CatalogManagement.Domain.Common.Exceptions;
using CatalogManagement.Domain.ProductGroupAggregate.Errors;
using CatalogManagement.Domain.ProductGroupAggregate.Exceptions;
using CatalogManagement.Domain.ProductGroupAggregate.ValueObjects;
using CatalogManagement.Domain.Tests.ProductGroupAggregate.Factories;

namespace CatalogManagement.Domain.Tests.ProductGroupAggregate;
public class ProductGroupValueObjectTests
{
    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void Creating_ProductGroupName_throw_Exception_When_Name_Null_OrWhiteSpace(string name)
    {
        var productGroupName = () => new ProductGroupName(name);

        productGroupName.Should().ThrowExactly<ProductGroupException>()
            .Which.Should().BeAssignableTo<DomainException>()
            .Which.Code.Should().Be(ProductGroupError.InvalidName.Code);
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void Creating_ProductGroupDescription_throw_Exception_When_Name_Null_OrWhiteSpace(string description)
    {
        var productGroupDescription = () => new ProductGroupDescription(description);

        productGroupDescription.Should().ThrowExactly<ProductGroupException>()
            .Which.Should().BeAssignableTo<DomainException>()
            .Which.Code.Should().Be(ProductGroupError.InvalidDescription.Code);
    }
    [Fact]
    public void Product_Should_Have_ProductGroupId_After_Creating()
    {
        var productGroup = ProductGroupFactory.Create();
        using (new AssertionScope())
        {
            productGroup.Id.Should().NotBeNull();
            productGroup.Id.Value.Should().NotBeEmpty();
        }
    }
}
