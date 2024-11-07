﻿using CatalogManagement.Application.Products;
using CatalogManagement.Application.Products.Queries.GetProductById;
using FluentAssertions;
using FluentAssertions.Execution;

namespace CatalogManagement.Application.Tests.Validations.ProductValidators;
public class GetProductByIdQuertValidatorTests
{
    [Fact]
    public void Validator_ReturnsValid_WhenProductIdValid()
    {
        var query = new GetProductByIdQuery(Guid.NewGuid());
        var validator = new GetProductByIdQueryValidator();

        var result = validator.Validate(query);

        using (AssertionScope scope = new())
        {
            result.IsValid.Should().BeTrue();
            result.Errors.Should().BeNullOrEmpty();
        }
    }

    [Theory]
    [MemberData(nameof(InvalidGuidData))]
    public void Validator_ReturnsValidationError_WhenProductIdInvalid(Guid productId)
    {
        var query = new GetProductByIdQuery(productId);
        var validator = new GetProductByIdQueryValidator();

        var result = validator.Validate(query);

        using (AssertionScope scope = new())
        {
            result.IsValid.Should().BeFalse();
            result.Errors.Count.Should().Be(1);
            result.Errors.Select(x => x.PropertyName).Should()
                .Contain([nameof(query.Id)]);
        }
    }

    public static IEnumerable<object[]> InvalidGuidData => new List<object[]> {
        new object[] { null },
        new object[] { Guid.Empty },
        new object[] { default(Guid) }
    };
}
