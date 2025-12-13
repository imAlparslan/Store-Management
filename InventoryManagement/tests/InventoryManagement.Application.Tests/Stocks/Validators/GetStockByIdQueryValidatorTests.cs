using InventoryManagement.Application.Stocks.Queries.GetStockById;
using InventoryManagement.Application.Tests.Common;

namespace InventoryManagement.Application.Tests.Stocks.Validators;

public class GetStockByIdQueryValidatorTests
{
    private readonly GetStockByIdQueryValidator _validator;

    public GetStockByIdQueryValidatorTests()
    {
        _validator = new();
    }

    [Fact]
    public async Task Validator_Returns_ValidResult_WhenGivenStockIdIsValid()
    {
        var query = new GetStockByIdQuery(Guid.CreateVersion7());

        var result = await _validator.ValidateAsync(query);

        result.IsValid.ShouldBeTrue();
        result.Errors.ShouldBeEmpty();
    }

    [Theory]
    [ClassData(typeof(InvalidGuidData))]
    public async Task Validator_Returns_FailResult_WhenGivenStockIdIsInvalid(Guid invalidGuid)
    {
        var query = new GetStockByIdQuery(invalidGuid);

        var result = await _validator.ValidateAsync(query);

        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldHaveSingleItem();
        result.Errors.Select(x => x.PropertyName)
            .ShouldContain(nameof(query.StockId));
    }
}
