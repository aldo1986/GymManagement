using MediatR;
using Microsoft.AspNetCore.Mvc;
using Application.Payments.Commands;
using Application.Application.Payments.Commands;
using Microsoft.AspNetCore.Authorization;

namespace Api.Controllers;
[Authorize]
[ApiController]
[Route("api/[controller]")]
public class PaymentsController : ControllerBase
{
    private readonly ISender _sender;

    public PaymentsController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost]
    public async Task<IActionResult> RegisterPayment(
        [FromBody] RegisterPaymentCommand command)
    {
        var paymentId = await _sender.Send(command);
        return Ok(paymentId); // Devolvemos el ID del nuevo pago
    }
}