﻿using CatalogManagement.Application.Common.Interfaces;
using CatalogManagement.Domain.ProductAggregate;
using CatalogManagement.SharedKernel;

namespace CatalogManagement.Application.Products;
public sealed record UpdateProductCommand(Guid Id, string ProductName, string ProductCode, string ProductDefinition)
    : ICommand<Result<Product>>;

