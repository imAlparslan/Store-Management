using CatalogManagement.Api.Mapping;
using CatalogManagement.Application.Products;
using CatalogManagement.Contracts.Products;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using static CatalogManagement.Api.ApiEndpoints;

namespace CatalogManagement.Api.Controllers;

public class ProductsController(IMediator mediator) : BaseApiController
{
    private readonly IMediator mediator = mediator;

    [HttpPost(ProductEndpoints.Create)]
    public async Task<IActionResult> Create([FromBody] CreateProductRequest request)
    {
        CreateProductCommand command = request.MapToCommand();

        var result = await mediator.Send(command);

        return result.Match(
            product => CreatedAtAction(nameof(GetById), new { Id = product.Id.Value }, product.MapToResponse()),
            Problem);

    }

    [HttpGet(ProductEndpoints.GetAll)]
    public async Task<IActionResult> GetAll()
    {
        var query = new GetAllProductsQuery();
        var result = await mediator.Send(query);

        return result.Match(products => Ok(products.Select(product => product.MapToResponse())),
            Problem);
    }

    [HttpGet(ProductEndpoints.GetById)]
    public async Task<IActionResult> GetById(Guid id)
    {
        var query = new GetProductByIdQuery(id);

        var result = await mediator.Send(query);

        return result.Match(product => Ok(product.MapToResponse()),
            Problem);
    }

    [HttpDelete(ProductEndpoints.Delete)]
    public async Task<IActionResult> DeleteById(Guid id)
    {
        var command = new DeleteProductByIdCommand(id);

        var result = await mediator.Send(command);

        return result.Match(suc => Ok(), Problem);
    }

    [HttpPut(ProductEndpoints.Update)]
    public async Task<IActionResult> Update([FromBody] UpdateProductRequest request, Guid id)
    {
        var command = request.MapToCommand(id);

        var result = await mediator.Send(command);

        return result.Match(product => Ok(product.MapToResponse()),
            Problem);
    }

    [HttpPost(ProductEndpoints.AddGroupToProduct)]
    public async Task<IActionResult> AddGroupToProduct(Guid productId, [FromBody] AddGroupToProductRequest request)
    {
        var command = request.MapToCommand(productId);

        var result = await mediator.Send(command);

        return result.Match(product => Ok(product.MapToResponse()),
            Problem);
    }

    [HttpPost(ProductEndpoints.RemoveFromProductGroup)]
    public async Task<IActionResult> RemoveGroupFromProduct(Guid productId, [FromBody] RemoveGroupFromProductRequest request)
    {
        var command = request.MapToCommand(productId);

        var result = await mediator.Send(command);

        return result.Match(product => Ok(product.MapToResponse()),
            Problem);
    }
}
