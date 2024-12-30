using CatalogManagement.Application.ProductGroups;
using CatalogManagement.Application.ProductGroups.Commands.AddProduct;
using CatalogManagement.Application.ProductGroups.Commands.RemoveProductFromProductGroup;
using CatalogManagement.Contracts.ProductGroups;
using CatalogManagement.Domain.ProductGroupAggregate;

namespace CatalogManagement.Api.Mapping;

public static class ProductGroupMappings
{
    public static CreateProductGroupCommand MapToCommand(this CreateProductGroupRequest request)
        => new CreateProductGroupCommand(request.Name, request.Description);

    public static UpdateProductGroupCommand MapToCommand(this UpdateProductGroupRequest request, Guid id)
        => new UpdateProductGroupCommand(id, request.Name, request.Description);


    public static AddProductToGroupCommand MapToCommand(this AddProductToProductGroupRequest request, Guid ProductGroupId)
        => new AddProductToGroupCommand(ProductGroupId, request.ProductId);

    public static ProductGroupResponse MapToResponse(this ProductGroup product)
        => new ProductGroupResponse(product.Id, product.Name, product.Description, product.ProductIds);

    public static RemoveProductFromProductGroupCommand MapToCommand(this RemoveProductFromProductGroupRequest request, Guid productGroupId)
        => new RemoveProductFromProductGroupCommand(productGroupId, request.ProductId);

}
