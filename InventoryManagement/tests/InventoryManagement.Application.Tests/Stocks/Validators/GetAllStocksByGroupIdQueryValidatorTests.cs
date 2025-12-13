using InventoryManagement.Application.Stocks.Queries.GetAllStocksByGroupId;
using InventoryManagement.Application.Stocks.Queries.GetStockByGroupId;
using InventoryManagement.Application.Tests.Common;

namespace InventoryManagement.Application.Tests.Stocks.Validators;

public class GetAllStocksByGroupIdQueryValidatorTests
{
    private GetAllStocksByGroupIdQueryValidator _validator;
    public GetAllStocksByGroupIdQueryValidatorTests()
    {
        _validator = new();
    }

    [Fact]
    public void Validator_Returns_ValidResult_When_GivenGroupIdIsValid()
    {
        var query = new GetAllStocksByGroupIdQuery(Guid.CreateVersion7());

        var validatorResult = _validator.Validate(query);

        validatorResult.IsValid.ShouldBeTrue();
        validatorResult.Errors.ShouldBeEmpty();
    }

    [Theory]
    [ClassData(typeof(InvalidGuidData))]
    public void Validator_Returns_ErrorResult_When_GroupIdIsInvalid(Guid invalidGuid)
    {
        var query = new GetAllStocksByGroupIdQuery(invalidGuid);

        var validatorResult = _validator.Validate(query);

        validatorResult.IsValid.ShouldBeFalse();
        validatorResult.Errors.ShouldHaveSingleItem();
        validatorResult.Errors.ShouldSatisfyAllConditions(
            x => x.Select(y => y.PropertyName).ShouldContain(nameof(query.GroupId))
        );
    }
}