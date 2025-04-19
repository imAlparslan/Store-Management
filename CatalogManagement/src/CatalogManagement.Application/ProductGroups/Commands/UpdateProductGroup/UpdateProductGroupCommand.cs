using CatalogManagement.Domain.ProductGroupAggregate;

namespace CatalogManagement.Application.ProductGroups;
public sealed record UpdateProductGroupCommand(Guid Id, string Name, string Description)
    : ICommand<Result<ProductGroup>>;
