using CatalogManagement.SharedKernel;
using MediatR;

namespace CatalogManagement.Application.ProductGroups;
public record DeleteProductGroupByIdCommand(Guid Id)
    : IRequest<Result<bool>>;
