using CatalogManagement.SharedKernel;
using Microsoft.AspNetCore.Mvc;

namespace CatalogManagement.Api.Controllers;

[ApiController]
public abstract class BaseApiController : ControllerBase
{

    protected IActionResult Problem(Error error)
    {
        return error.Type switch
        {
            ErrorType.Validation => BadRequest(error.Description),
            ErrorType.NotDeleted => NotFound(error.Description),
            ErrorType.NotFound => NotFound(error.Description),
            _ => new ObjectResult(500)
        };
    }
}
