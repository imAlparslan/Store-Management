using CatalogManagement.Domain.ProductGroupAggregate.Errors;
using CatalogManagement.Domain.ProductGroupAggregate.Exceptions;
using CatalogManagement.Domain.ProductGroupAggregate.ValueObjects;
using CatalogManagement.Domain.Tests.Common.InvalidData;
using CatalogManagement.Domain.Tests.ProductGroupAggregate.Factories;

namespace CatalogManagement.Domain.Tests.ProductGroupAggregate;

public class ProductGroupValueObjectTests
{
    [Theory]
    [ClassData(typeof(InvalidStringData))]
    public void Creating_ProductGroupName_throw_Exception_When_Name_Null_OrWhiteSpace(string name)
    {
        var productGroupName = () => new ProductGroupName(name);

        productGroupName.ShouldThrow<ProductGroupException>()
            .ShouldSatisfyAllConditions(
                x => x.Message.ShouldBe(ProductGroupError.InvalidName.Description),
                x => x.Code.ShouldBe(ProductGroupError.InvalidName.Code),
                x => x.ShouldBeAssignableTo<DomainException>()
            );
    }

    [Theory]
    [ClassData(typeof(InvalidStringData))]
    public void Creating_ProductGroupDescription_throw_Exception_When_Name_Null_OrWhiteSpace(string description)
    {
        var productGroupDescription = () => new ProductGroupDescription(description);

        productGroupDescription.ShouldThrow<ProductGroupException>()
            .ShouldSatisfyAllConditions(
                x => x.Message.ShouldBe(ProductGroupError.InvalidDescription.Description),
                x => x.Code.ShouldBe(ProductGroupError.InvalidDescription.Code),
                x => x.ShouldBeAssignableTo<DomainException>()
            );
    }
    [Fact]
    public void Product_Should_Have_ProductGroupId_After_Creating()
    {
        var productGroup = ProductGroupFactory.Create();

        productGroup.ShouldNotBeNull();
        productGroup.Id.ShouldNotBeNull();
    }
}
