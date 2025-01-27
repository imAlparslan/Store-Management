using MediatR;
using Microsoft.AspNetCore.Mvc;
using StoreDefinition.Api.Mapping;
using StoreDefinition.Application.Groups.Commands.DeleteGroup;
using StoreDefinition.Application.Groups.Queries.GetAllGroups;
using StoreDefinition.Application.Groups.Queries.GetGroupById;
using StoreDefinition.Contracts.Groups;
using static StoreDefinition.Api.ApiEndpoints;

namespace StoreDefinition.Api.Controllers;

public class GroupsController(IMediator mediator) : BaseApiController
{
    private readonly IMediator mediator = mediator;


    [HttpPost(GroupsEndpoints.Create)]
    public async Task<IActionResult> Create([FromBody] CreateGroupRequest request)
    {
        var command = request.MapToCommand();
        var result = await mediator.Send(command);
        return result.Match(
            group => CreatedAtAction(nameof(GetById), new { Id = group.Id.Value }, group.MapToResponse()),
            Problem);
    }
    [HttpPut(GroupsEndpoints.Update)]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateGroupRequest request)
    {
        var command = request.MapToCommand(id);
        var result = await mediator.Send(command);
        return result.Match(
            group => Ok(group.MapToResponse()),
            Problem);
    }

    [HttpDelete(GroupsEndpoints.Delete)]
    public async Task<IActionResult> Delete(Guid id)
    {
        var command = new DeleteGroupCommand(id);
        var result = await mediator.Send(command);
        return result.Match(
            group => Ok(group),
            Problem);
    }

    [HttpGet(GroupsEndpoints.GetById)]
    public async Task<IActionResult> GetById(Guid id)
    {
        var query = new GetGroupByIdQuery(id);
        var result = await mediator.Send(query);
        return result.Match(
            group => Ok(group.MapToResponse()),
            Problem);
    }

    [HttpGet(GroupsEndpoints.GetAll)]
    public async Task<IActionResult> GetAll()
    {
        var query = new GetAllGroupsQuery();
        var result = await mediator.Send(query);
        return result.Match(
            groups => Ok(groups.Select(g => g.MapToResponse())),
            Problem);
    }
}
