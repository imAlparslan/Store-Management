using CatalogManagement.Domain.ProductGroupAggregate;
using CatalogManagement.SharedKernel;
using MediatR;

namespace CatalogManagement.Application.ProductGroups;
public record CreateProductGroupCommand(string Name, string Description)
    : IRequest<Result<ProductGroup>>;
