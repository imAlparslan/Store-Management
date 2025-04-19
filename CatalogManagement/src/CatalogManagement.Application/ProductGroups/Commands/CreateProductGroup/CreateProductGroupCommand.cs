using CatalogManagement.Domain.ProductGroupAggregate;

namespace CatalogManagement.Application.ProductGroups;
public sealed record CreateProductGroupCommand(string Name, string Description)
    : ICommand<Result<ProductGroup>>;
