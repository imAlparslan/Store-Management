using CatalogManagement.Application.ProductGroups;
using CatalogManagement.Contracts.ProductGroups;
using CatalogManagement.Domain.ProductGroupAggregate;

namespace CatalogManagement.Api.Mapping;

public static class ProductGroupMappings
{
    public static ProductGroupResponse MapToResponse(this ProductGroup product)
        => new ProductGroupResponse(product.Id, product.Name, product.Description, product.ProductIds);
    public static CreateProductGroupCommand MapToCommand(this CreateProductGroupRequest request)
        => new CreateProductGroupCommand(request.Name, request.Description);

    public static UpdateProductGroupCommand MapToCommand(this UpdateProductGroupRequest request, Guid id)
        => new UpdateProductGroupCommand(id, request.Name, request.Description);
}
