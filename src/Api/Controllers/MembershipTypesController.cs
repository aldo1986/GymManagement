using MediatR;
using Microsoft.AspNetCore.Mvc;
using Application.Application.MembershipTypes.Commands;
using Microsoft.AspNetCore.Authorization;
using Application.MembershipTypes.Queries;

namespace Api.Controllers;
[Authorize]
[ApiController]
[Route("api/[controller]")]
public class MembershipTypesController : ControllerBase
{
    private readonly ISender _sender;

    public MembershipTypesController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost]
    public async Task<IActionResult> CreateMembershipType(
        [FromBody] CreateMembershipTypeCommand command)
    {
        var membershipTypeId = await _sender.Send(command);

        // Devolvemos un 201 Created con la ubicación del nuevo recurso.
        // Necesitaremos un endpoint GET para que esto funcione, pero lo podemos añadir después.
        return CreatedAtAction(nameof(GetMembershipTypeById), new { id = membershipTypeId }, null);
    }

    // NOTA: Este endpoint aún no está implementado, pero lo necesitamos para CreatedAtAction.
    // Lo podemos dejar así por ahora.
    [HttpGet("{id}")]
    public IActionResult GetMembershipTypeById(int id)
    {
        // Lógica para obtener el tipo de membresía por ID (se implementará después)
        return Ok($"Lógica pendiente para obtener el tipo de membresía {id}");
    }
    [HttpGet]
    public async Task<IActionResult> GetAllMembershipTypes()
    {
        var query = new GetAllMembershipTypesQuery();
        var result = await _sender.Send(query);
        return Ok(result);
    }
}