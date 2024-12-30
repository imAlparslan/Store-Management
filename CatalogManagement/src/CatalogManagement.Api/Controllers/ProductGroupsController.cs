using CatalogManagement.Api.Mapping;
using CatalogManagement.Application.ProductGroups;
using CatalogManagement.Contracts.ProductGroups;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using static CatalogManagement.Api.ApiEndpoints;

namespace CatalogManagement.Api.Controllers;

public class ProductGroupsController(IMediator mediator) : BaseApiController
{
    private readonly IMediator mediator = mediator;

    [HttpPost(ProductGroupEndpoints.Create)]
    public async Task<IActionResult> Create(CreateProductGroupRequest request)
    {
        var command = request.MapToCommand();
        var result = await mediator.Send(command);
        return result.Match(
            productGroup => CreatedAtAction(nameof(GetById), new { Id = productGroup.Id.Value }, productGroup.MapToResponse()),
            Problem);
    }

    [HttpPut(ProductGroupEndpoints.Update)]
    public async Task<IActionResult> Update(Guid id, UpdateProductGroupRequest request)
    {
        var command = request.MapToCommand(id);
        var result = await mediator.Send(command);
        return result.Match(
            productGroup => Ok(productGroup.MapToResponse()),
            Problem);
    }

    [HttpGet(ProductGroupEndpoints.GetAll)]
    public async Task<IActionResult> GetAll()
    {
        var command = new GetAllProductGroupsQuery();
        var result = await mediator.Send(command);
        return result.Match(
            productGroups => Ok(productGroups.Select(productGroup => productGroup.MapToResponse())),
            Problem);
    }

    [HttpGet(ProductGroupEndpoints.GetById)]
    public async Task<IActionResult> GetById(Guid id)
    {
        var command = new GetProductGroupByIdQuery(id);
        var result = await mediator.Send(command);
        return result.Match(
            productGroup => Ok(productGroup.MapToResponse()),
            Problem);
    }

    [HttpDelete(ProductGroupEndpoints.Delete)]
    public async Task<IActionResult> Delete(Guid id)
    {
        var command = new DeleteProductGroupByIdCommand(id);
        var result = await mediator.Send(command);
        return result.Match(
            success => Ok(success),
            Problem);
    }

    [HttpPost(ProductGroupEndpoints.AddProductToProductGroup)]
    public async Task<IActionResult> AddProductToProductGroup(Guid productGroupId, [FromBody] AddProductToProductGroupRequest request)
    {
        var command = request.MapToCommand(productGroupId);

        var result = await mediator.Send(command);

        return result.Match(productGroup => Ok(productGroup.MapToResponse()),
            Problem);
    }

    [HttpPost(ProductGroupEndpoints.RemoveProductFromProductGroup)]
    public async Task<IActionResult> RemoveProductFromProductGroup(Guid productGroupId, [FromBody] RemoveProductFromProductGroupRequest request)
    {
        var command = request.MapToCommand(productGroupId);
        var result = await mediator.Send(command);
        return result.Match(productGroup => Ok(productGroup.MapToResponse()),
            Problem);
    }
}
