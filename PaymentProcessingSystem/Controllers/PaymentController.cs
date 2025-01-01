using Catel.Data;
using MediatR;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using PaymentProcessingSystem.Core;
using PaymentProcessingSystem.Core.Commands;
using System.Net;

namespace PaymentProcessingSystem.Controllers;

[Produces("application/json")]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
[ProducesResponseType(typeof(CommonResponse), 417)]
[ProducesResponseType(typeof(CommonResponse), 400)]
[ProducesResponseType(typeof(CommonResponse), 401)]
[ProducesResponseType(typeof(CommonResponse), 500)]
public class PaymentController(IMediator mediator) : Controller
{   
    [HttpPost("Create")]
    [ProducesResponseType(typeof(CommonResponse), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(CommonResponse), (int)HttpStatusCode.BadRequest)]
     public async Task<IActionResult> CreatePolicy([FromHeader(Name = "x-brandid")] string brandid,
        [FromBody] CreatePaymentCommand command, [FromHeader(Name = "x-requestid")] string requestId)
    {
      
        var commandResult = await mediator.Send(command);

        return commandResult != null ? Ok(commandResult) : BadRequest();
    }
    public IActionResult Index()
    {
        return View();
    }
}
