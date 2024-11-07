﻿using CatalogManagement.Domain.ProductGroupAggregate;
using CatalogManagement.SharedKernel;
using MediatR;

namespace CatalogManagement.Application.ProductGroups;
public record GetAllProductGroupsQuery()
    : IRequest<Result<IEnumerable<ProductGroup>>>;
