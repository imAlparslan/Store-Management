﻿using Ardalis.GuardClauses;
using CatalogManagement.Domain.Common.Models;
using CatalogManagement.Domain.ProductGroupAggregate.Errors;
using CatalogManagement.Domain.ProductGroupAggregate.Exceptions;

namespace CatalogManagement.Domain.ProductGroupAggregate.ValueObjects;
public sealed class ProductGroupDescription : ValueObject
{
    public string Value { get; private set; }
    public ProductGroupDescription(string description)
    {
        Value = Guard.Against.NullOrWhiteSpace(
            description,
            exceptionCreator: () => ProductGroupException.Create(ProductGroupError.InvalidDescription));
    }
    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}