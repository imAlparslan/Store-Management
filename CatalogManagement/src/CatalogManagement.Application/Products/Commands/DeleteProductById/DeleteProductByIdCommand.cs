using CatalogManagement.SharedKernel;
using MediatR;

namespace CatalogManagement.Application.Products.Commands.DeleteProductById;
public record DeleteProductByIdCommand(Guid Id) : IRequest<Result<bool>>;
