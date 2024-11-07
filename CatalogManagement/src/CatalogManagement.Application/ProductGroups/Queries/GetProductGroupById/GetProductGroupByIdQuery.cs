using CatalogManagement.Domain.ProductGroupAggregate;
using CatalogManagement.SharedKernel;
using MediatR;

namespace CatalogManagement.Application.ProductGroups;
public record GetProductGroupByIdQuery(Guid Id)
    : IRequest<Result<ProductGroup>>;
