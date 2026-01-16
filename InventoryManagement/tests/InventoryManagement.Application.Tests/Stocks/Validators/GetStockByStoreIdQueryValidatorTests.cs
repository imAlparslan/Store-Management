using InventoryManagement.Application.Stocks.Queries.GetStockByStoreId;
using InventoryManagement.Application.Tests.Common;

namespace InventoryManagement.Application.Tests.Stocks.Validators;

public class GetStockByStoreIdQueryValidatorTests
{
    private GetStockByStoreIdQueryValidator _validator;
    public GetStockByStoreIdQueryValidatorTests()
    {
        _validator = new();
    }
    [Fact]
    public async Task Validator_Returns_ValidResult_WhenGivenStoreIdIsNotValid()
    {
        var query = new GetStockByStoreIdQuery(Guid.CreateVersion7());

        var result = await _validator.ValidateAsync(query);

        result.IsValid.ShouldBeTrue();
        result.Errors.ShouldBeEmpty();
    }

    [Theory]
    [ClassData(typeof(InvalidGuidData))]
    public async Task Validator_Returns_FailResult_WhenGivenStoreIdIsNotValid(Guid invalidGuid)
    {
        var query = new GetStockByStoreIdQuery(invalidGuid);

        var result = await _validator.ValidateAsync(query);

        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldHaveSingleItem();
        result.Errors.Select(x => x.PropertyName)
            .ShouldContain(nameof(query.StoreId));
    }
}