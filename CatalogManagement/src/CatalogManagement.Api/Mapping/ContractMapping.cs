using CatalogManagement.Application.ProductGroups;
using CatalogManagement.Application.ProductGroups.Commands.AddProduct;
using CatalogManagement.Application.Products;
using CatalogManagement.Application.Products.Commands.AddGroup;
using CatalogManagement.Contracts.ProductGroups;
using CatalogManagement.Contracts.Products;
using CatalogManagement.Domain.ProductAggregate;
using CatalogManagement.Domain.ProductGroupAggregate;

namespace CatalogManagement.Api.Mapping;

public static class ContractMapping
{

    public static CreateProductCommand MapToCommand(this CreateProductRequest request)
        => new CreateProductCommand(request.ProductName, request.ProductCode, request.ProductDefinition);


    public static UpdateProductCommand MapToCommand(this UpdateProductRequest request, Guid id)
    {
        return new UpdateProductCommand(id, request.ProductName, request.ProductCode, request.ProductDefinition);
    }

    public static ProductResponse MapToResponse(this Product product)
    {
        return new ProductResponse(product.Id, product.Name, product.Code, product.Definition, product.GroupIds);
    }

    public static CreateProductGroupCommand MapToCommand(this CreateProductGroupRequest request)
    {
        return new CreateProductGroupCommand(request.Name, request.Description);
    }

    public static UpdateProductGroupCommand MapToCommand(this UpdateProductGroupRequest request, Guid id)
    {
        return new UpdateProductGroupCommand(id, request.Name, request.Description);
    }

    public static AddGroupCommand MapToCommand(this AddGroupRequest request, Guid ProductId)
    {
        return new AddGroupCommand(ProductId, request.ProductGroupId);
    }

    public static AddProductCommand MapToCommand(this AddProductRequest request, Guid ProductGroupId)
    {
        return new AddProductCommand(ProductGroupId, request.ProductId);
    }

    public static ProductGroupResponse MapToResponse(this ProductGroup product)
    {
        return new ProductGroupResponse(product.Id, product.Name, product.Description, product.ProductIds);
    }


}
