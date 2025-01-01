using Microsoft.AspNetCore.Mvc;
using PaymentProcessingSystem.Core;

namespace PaymentProcessingSystem.Controllers;

[Produces("application/json")]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
[ProducesResponseType(typeof(CommonResponse), 417)]
[ProducesResponseType(typeof(CommonResponse), 400)]
[ProducesResponseType(typeof(CommonResponse), 401)]
[ProducesResponseType(typeof(CommonResponse), 500)]
[ValidateModel()]
public class PaymentController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
