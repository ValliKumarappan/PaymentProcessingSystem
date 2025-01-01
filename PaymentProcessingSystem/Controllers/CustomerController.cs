using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PaymentProcessingSystem.Core;
using PaymentProcessingSystem.Core.Commands;
using PaymentProcessingSystem.Core.Queries;
using PaymentProcessingSystem.SharedKernel.FilterModels;
using System.Net;

namespace CustomerProcessingSystem.Controllers;

[Produces("application/json")]
[Route("api/[controller]")]
[ProducesResponseType(typeof(CommonResponse), 417)]
[ProducesResponseType(typeof(CommonResponse), 400)]
[ProducesResponseType(typeof(CommonResponse), 401)]
[ProducesResponseType(typeof(CommonResponse), 500)]
[AllowAnonymous]
public class CustomerController(IMediator mediator, ICustomerQueries queries) : Controller
{
    [AllowAnonymous]
    [HttpPost("Create")]
    [ProducesResponseType(typeof(CommonResponse), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(CommonResponse), (int)HttpStatusCode.BadRequest)]
     public async Task<IActionResult> CreatePolicy([FromBody] CreateCustomerCommand command)
    {
      
        var commandResult = await mediator.Send(command);

        return commandResult != null ? Ok(commandResult) : BadRequest();
    }

    [AllowAnonymous]
    [HttpPut("Update")]
    [ProducesResponseType(typeof(CommonResponse), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(CommonResponse), (int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> UpdatePolicy([FromBody] UpdateCustomerCommand command)
    {

        var commandResult = await mediator.Send(command);

        return commandResult != null ? Ok(commandResult) : BadRequest();
    }

    [HttpGet("List")]
    [ProducesResponseType(typeof(CommonResponse), 200)]
    [ProducesResponseType(typeof(CommonResponse), 409)]
    [ProducesResponseType(typeof(CommonResponse), 404)]
    public async Task<IActionResult> GetList([FromQuery] CustomerFilters command)
    {
        var commandResult = await queries.GetList(command);

        return StatusCode(commandResult.StatusCode, commandResult);

    }

}
