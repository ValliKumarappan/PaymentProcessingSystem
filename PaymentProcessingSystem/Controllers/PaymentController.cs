using Catel.Data;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using PaymentProcessingSystem.Core;
using PaymentProcessingSystem.Core.Commands;
using System.Net;

namespace PaymentProcessingSystem.Controllers;

[Produces("application/json")]
[Route("api/[controller]")]
[ProducesResponseType(typeof(CommonResponse), 417)]
[ProducesResponseType(typeof(CommonResponse), 400)]
[ProducesResponseType(typeof(CommonResponse), 401)]
[ProducesResponseType(typeof(CommonResponse), 500)]
[AllowAnonymous]
public class PaymentController(IMediator mediator) : Controller
{
    [AllowAnonymous]
    [HttpPost("Create")]
    [ProducesResponseType(typeof(CommonResponse), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(CommonResponse), (int)HttpStatusCode.BadRequest)]
     public async Task<IActionResult> CreatePolicy([FromBody] CreatePaymentCommand command)
    {
      
        var commandResult = await mediator.Send(command);

        return commandResult != null ? Ok(commandResult) : BadRequest();
    }

    [AllowAnonymous]
    [HttpPost("Update")]
    [ProducesResponseType(typeof(CommonResponse), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(CommonResponse), (int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> UpdatePolicy([FromBody] UpdatePaymentCommand command)
    {

        var commandResult = await mediator.Send(command);

        return commandResult != null ? Ok(commandResult) : BadRequest();
    }
}
