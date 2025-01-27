using CatalogManagement.Application.Products;
using CatalogManagement.Application.Products.Commands.AddGroup;
using CatalogManagement.Application.Products.Commands.RemoveGroupFromProduct;
using CatalogManagement.Contracts.Products;
using CatalogManagement.Domain.ProductAggregate;

namespace CatalogManagement.Api.Mapping;

public static class ProductMappings
{
    public static ProductResponse MapToResponse(this Product product)
        => new ProductResponse(product.Id, product.Name, product.Code, product.Definition, product.GroupIds);
    public static CreateProductCommand MapToCommand(this CreateProductRequest request)
       => new CreateProductCommand(request.ProductName, request.ProductCode, request.ProductDefinition, request.GroupIds);

    public static UpdateProductCommand MapToCommand(this UpdateProductRequest request, Guid id)
       => new UpdateProductCommand(id, request.ProductName, request.ProductCode, request.ProductDefinition);

    public static AddGroupToProductCommand MapToCommand(this AddGroupToProductRequest request, Guid productId)
        => new AddGroupToProductCommand(productId, request.ProductGroupId);

    public static RemoveGroupFromProductCommand MapToCommand(this RemoveGroupFromProductRequest request, Guid productId)
        => new RemoveGroupFromProductCommand(request.ProductGroupId, productId);
}
