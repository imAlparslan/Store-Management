using CatalogManagement.Application.Common.Interfaces;
using CatalogManagement.SharedKernel;

namespace CatalogManagement.Application.Products;
public sealed record DeleteProductByIdCommand(Guid Id)
    : ICommand<Result<bool>>;
