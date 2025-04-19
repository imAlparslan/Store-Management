namespace CatalogManagement.Application.ProductGroups;
public sealed record DeleteProductGroupByIdCommand(Guid Id)
    : ICommand<Result<bool>>;
