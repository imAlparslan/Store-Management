using InventoryManagement.Api.Mapper;
using InventoryManagement.Application.Stocks.Commands.AddStockItem;
using InventoryManagement.Contracts.Stocks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using static InventoryManagement.Api.ApiEndpoints;

namespace InventoryManagement.Api.Controllers;

public class StocksController(IMediator mediator) : BaseApiController
{
    private readonly IMediator _mediator = mediator;

    [HttpPost(StockEndpoints.AddItem)]
    public async Task<IActionResult> AddStockItem(AddStockItemRequest request)
    {
        AddStockItemCommand command = request.MapToCommand();

        var result = await _mediator.Send(command);

        return result.Match(
            stock => Ok(stock.MapToResponse()),
            Problem);
    }


    [HttpGet(StockEndpoints.GetAllStocksByGroupId)]
    public async Task<IActionResult> GetAllStocksByGroupId([FromQuery] GetAllStocksByGroupIdRequest request)
    {
        var query = request.MapToQuery();

        var result = await _mediator.Send(query);

        return result.Match(
            stocks => Ok(stocks.Select(stock => stock.MapToResponse())),
            Problem);
    }
}
