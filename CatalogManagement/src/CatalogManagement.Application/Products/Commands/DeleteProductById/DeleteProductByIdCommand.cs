using CatalogManagement.SharedKernel;
using MediatR;

namespace CatalogManagement.Application.Products;
public record DeleteProductByIdCommand(Guid Id) : IRequest<Result<bool>>;
