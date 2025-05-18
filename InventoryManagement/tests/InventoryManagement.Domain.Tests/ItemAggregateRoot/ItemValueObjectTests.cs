namespace InventoryManagement.Domain.Tests.ItemAggregateRoot;

public class ItemValueObjectTests
{
    [Theory]
    [ClassData(typeof(InvalidString))]
    public void Creating_ProductDefinition_ShouldThrowException_WhenProductName_NullOrWhiteSpace(string? invalid)
    {
        var createProductDefinition = () => new ProductDefinition(
            name: invalid!,
            code: Constants.ValidProductCode,
            definition: Constants.ValidProductDefinition);

        Should.Throw<DomainException>(createProductDefinition)
               .ShouldSatisfyAllConditions(
                   x => x.Message.ShouldBe(ItemErrors.InvalidProductName.Description),
                   x => x.Code.ShouldBe(ItemErrors.InvalidProductName.Code));
    }

    [Theory]
    [ClassData(typeof(InvalidString))]
    public void Creating_ProductDefinition_ShouldThrowException_WhenProductCode_NullOrWhiteSpace(string? invalid)
    {
        var createProductDefinition = () => new ProductDefinition(
            name: Constants.ValidProductName,
            code: invalid!, 
            definition: Constants.ValidProductDefinition);

        Should.Throw<DomainException>(createProductDefinition)
           .ShouldSatisfyAllConditions(
               x => x.Message.ShouldBe(ItemErrors.InvalidProductCode.Description),
               x => x.Code.ShouldBe(ItemErrors.InvalidProductCode.Code));
    }

    [Theory]
    [ClassData(typeof(InvalidString))]
    public void Creating_ProductDefinition_ShouldThrowException_WhenProductDefinition_NullOrWhiteSpace(string? invalid)
    {
        var createProductDefinition = () => new ProductDefinition(
            name: Constants.ValidProductName,
            code: Constants.ValidProductCode,
            definition: invalid!);

        Should.Throw<DomainException>(createProductDefinition)
            .ShouldSatisfyAllConditions(
                x => x.Message.ShouldBe(ItemErrors.InvalidProductDefinition.Description), 
                x => x.Code.ShouldBe(ItemErrors.InvalidProductDefinition.Code));
    }
}
