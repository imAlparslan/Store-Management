using CatalogManagement.Application.ProductGroups;
using CatalogManagement.Contracts.ProductGroups;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using static CatalogManagement.Api.ApiEndpoints;

namespace CatalogManagement.Api.Controllers;

public class ProductGroupsController : BaseApiController
{

    private readonly IMediator mediator;

    public ProductGroupsController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [HttpPost(ProductGroupEndpoints.Create)]
    public async Task<IActionResult> Create(CreateProductGroupRequest request)
    {
        var command = new CreateProductGroupCommand(request.Name, request.Description);
        var result = await mediator.Send(command);
        return result.Match(
            success => CreatedAtAction(nameof(GetById),
                new { Id = success.Id.Value },
                new ProductGroupResponse(success.Id, success.Name, success.Description)),
            Problem);
    }

    [HttpPut(ProductGroupEndpoints.Update)]
    public async Task<IActionResult> Update(Guid id, UpdateProductGroupRequest request)
    {
        var command = new UpdateProductGroupCommand(id, request.Name, request.Description);
        var result = await mediator.Send(command);
        return result.Match(
            success => Ok(new ProductGroupResponse(success.Id, success.Name, success.Description)),
            Problem);
    }

    [HttpGet(ProductGroupEndpoints.GetAll)]
    public async Task<IActionResult> GetAll()
    {
        var command = new GetAllProductGroupsQuery();
        var result = await mediator.Send(command);
        return result.Match(
            success => Ok(success.Select(group => new ProductGroupResponse(group.Id, group.Name, group.Description))),
            Problem);
    }

    [HttpGet(ProductGroupEndpoints.GetById)]
    public async Task<IActionResult> GetById(Guid id)
    {
        var command = new GetProductGroupByIdQuery(id);
        var result = await mediator.Send(command);
        return result.Match(
            success => Ok(new ProductGroupResponse(success.Id, success.Name, success.Description)),
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
}
