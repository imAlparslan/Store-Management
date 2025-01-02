﻿using CatalogManagement.Application.Common.Interfaces;
using CatalogManagement.Domain.ProductGroupAggregate;
using CatalogManagement.SharedKernel;

namespace CatalogManagement.Application.ProductGroups;
public sealed record UpdateProductGroupCommand(Guid Id, string Name, string Description)
    : ICommand<Result<ProductGroup>>;
