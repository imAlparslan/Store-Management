namespace CatalogManagement.Application.Products;
public sealed record DeleteProductByIdCommand(Guid Id)
    : ICommand<Result<bool>>;
