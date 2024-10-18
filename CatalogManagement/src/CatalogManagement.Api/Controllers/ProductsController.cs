using CatalogManagement.Application.Products.Commands.CreateProduct;
using CatalogManagement.Application.Products.Commands.DeleteProductById;
using CatalogManagement.Application.Products.Commands.UpdateProduct;
using CatalogManagement.Application.Products.Queries.GetAllProducts;
using CatalogManagement.Application.Products.Queries.GetProductById;
using CatalogManagement.Contracts.Products;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using static CatalogManagement.Api.ApiEndpoints;

namespace CatalogManagement.Api.Controllers;

public class ProductsController : BaseApiController
{

    private readonly IMediator mediator;

    public ProductsController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [HttpPost(ProductEndpoints.Create)]
    [ProducesResponseType(typeof(ProductResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateProductRequest request)
    {
        var command = new CreateProductCommand(request.ProductName, request.ProductCode, request.ProductDefinition);

        var result = await mediator.Send(command);

        return result.Match(
            suc => CreatedAtAction(nameof(GetById), new { Id = suc.Id.Value }, new ProductResponse(suc.Name, suc.Code, suc.Definition)),
            fail => Problem(fail));

    }

    [HttpGet(ProductEndpoints.GetAll)]
    [ProducesResponseType(typeof(ProductResponse), StatusCodes.Status200OK)]

    public async Task<IActionResult> GetAll()
    {
        var query = new GetAllProductsQuery();

        var result = await mediator.Send(query);

        return result.Match(suc => Ok(suc.Select(product => new ProductResponse(product.Name, product.Code, product.Definition))),
            fail => Problem(fail));
    }

    [HttpGet(ProductEndpoints.GetById)]
    [ProducesResponseType(typeof(ProductResponse), StatusCodes.Status200OK)]

    public async Task<IActionResult> GetById(Guid id)
    {
        var query = new GetProductByIdQuery(id);

        var result = await mediator.Send(query);

        return result.Match(suc => Ok(new ProductResponse(suc.Name, suc.Code, suc.Definition)),
            fail => Problem(fail));
    }

    [HttpDelete(ProductEndpoints.Delete)]
    public async Task<IActionResult> DeleteById(Guid id)
    {
        var command = new DeleteProductByIdCommand(id);

        var result = await mediator.Send(command);

        return result.Match(suc => Ok(), fail => Problem(fail));
    }

    
    [HttpPut(ProductEndpoints.Update)]
    [ProducesResponseType(typeof(ProductResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> Update([FromBody] UpdateProductRequest request, Guid id)
    {
        var command = new UpdateProductCommand(id, request.ProductName, request.ProductCode, request.ProductDefinition);

        var result = await mediator.Send(command);

        return result.Match(suc => Ok(new ProductResponse(suc.Name, suc.Code, suc.Definition)), fail => Problem(fail));
    }
}
