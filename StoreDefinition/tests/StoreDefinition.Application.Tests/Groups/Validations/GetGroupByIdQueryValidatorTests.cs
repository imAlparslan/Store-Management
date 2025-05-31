using StoreDefinition.Application.Groups.Queries.GetGroupById;

namespace StoreDefinition.Application.Tests.Groups.Validations;

public class GetGroupByIdQueryValidatorTests
{
    private readonly GetGroupByIdQueryValidator validator;
    public GetGroupByIdQueryValidatorTests()
    {
        validator = new GetGroupByIdQueryValidator();
    }

    [Fact]
    public void Validator_ReturnsTrue_WhenQueryValid()
    {
        var query = new GetGroupByIdQuery(Guid.NewGuid());

        var result = validator.Validate(query);

        result.IsValid.ShouldBeTrue();
    }

    [Fact]
    public void Validator_ReturnsFalse_WhenQueryInvalid()
    {
        var query = new GetGroupByIdQuery(Guid.Empty);

        var result = validator.Validate(query);

        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldHaveSingleItem();
        result.Errors.Select(x => x.PropertyName)
            .ShouldContain(nameof(query.GroupId));
    }
}
