using CatalogManagement.Domain.ProductGroupAggregate;
using CatalogManagement.SharedKernel;
using MediatR;

namespace CatalogManagement.Application.ProductGroups;
public record UpdateProductGroupCommand(Guid Id, string Name, string Description)
    : IRequest<Result<ProductGroup>>;
