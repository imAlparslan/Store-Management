using MediatR;
using Microsoft.AspNetCore.Mvc;
using StoreDefinition.Api.Mapping;
using StoreDefinition.Application.Shops.Commands.DeleteShop;
using StoreDefinition.Application.Shops.Queries.GetAllShops;
using StoreDefinition.Application.Shops.Queries.GetShopById;
using StoreDefinition.Contracts.Shops;
using static StoreDefinition.Api.ApiEndpoints;

namespace StoreDefinition.Api.Controllers;
public class ShopsController(IMediator mediator) : BaseApiController
{
    private readonly IMediator mediator = mediator;


    [HttpPost(ShopsEndpoints.Create)]
    public async Task<IActionResult> Create([FromBody] CreateShopRequest request)
    {
        var command = request.MapToCommand();
        var result = await mediator.Send(command);

        return result.Match(
            shop => CreatedAtAction(nameof(GetById), new { Id = shop.Id.Value }, shop.MapToResponse()),
            Problem);
    }

    [HttpPut(ShopsEndpoints.Update)]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateShopRequest request)
    {
        var command = request.MapToCommand(id);
        var result = await mediator.Send(command);

        return result.Match(
            shop => Ok(shop.MapToResponse()),
            Problem);
    }
    [HttpDelete(ShopsEndpoints.Delete)]
    public async Task<IActionResult> Delete(Guid id)
    {
        var command = new DeleteShopCommand(id);
        var result = await mediator.Send(command);

        return result.Match(
            shop => Ok(shop),
            Problem);
    }

    [HttpPost(ShopsEndpoints.AddGroupToShop)]
    public async Task<IActionResult> AddGroupToShop(Guid id, [FromBody] AddGroupToShopRequest request)
    {
        var command = request.MapToCommand(id);
        var result = await mediator.Send(command);

        return result.Match(shop => Ok(shop.MapToResponse()), Problem);
    }

    [HttpPost(ShopsEndpoints.RemoveGroupFromShop)]
    public async Task<IActionResult> RemoveGroupFromShop(Guid id, [FromBody] RemoveGroupFromShopRequest request)
    {
        var command = request.MapToCommand(id);
        var result = await mediator.Send(command);

        return result.Match(shop => Ok(shop.MapToResponse()), Problem);
    }

    [HttpGet(ShopsEndpoints.GetAll)]
    public async Task<IActionResult> GetAllShops()
    {
        var query = new GetAllShopsQuery();
        var result = await mediator.Send(query);

        return result.Match(shops => Ok(shops.Select(x => x.MapToResponse())), Problem);
    }

    [HttpGet(ShopsEndpoints.GetById)]
    public async Task<IActionResult> GetById(Guid id)
    {
        var query = new GetShopByIdQuery(id);
        var result = await mediator.Send(query);
        return result.Match(
            shop => Ok(shop.MapToResponse()),
            Problem);
    }
}
