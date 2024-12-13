namespace CatalogManagement.Contracts.Products;
public record ProductResponse(
    Guid Id,
    string ProductName,
    string ProductCode,
    string ProductDefinition,
    IReadOnlyList<Guid> GroupIds);
