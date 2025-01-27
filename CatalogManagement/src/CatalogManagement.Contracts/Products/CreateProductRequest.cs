namespace CatalogManagement.Contracts.Products;
public record CreateProductRequest(string ProductName,
                                   string ProductCode,
                                   string ProductDefinition,
                                   IReadOnlyList<Guid> GroupIds);
