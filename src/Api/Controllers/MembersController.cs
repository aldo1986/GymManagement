using MediatR;
using Application.Application.Members.Commands;
using Application.Application.Members.Queries;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;


namespace Api.Controllers;
[Authorize]
[ApiController]
[Route("api/[controller]")]
public class MembersController : ControllerBase
{
    private readonly ISender _sender;

    public MembersController(ISender sender)
    {
        _sender = sender;
    }

   
    [HttpGet("{id}")]
    public async Task<IActionResult> GetMemberById(int id)
    {
        var query = new GetMemberByIdQuery(id);

       
        var memberResponse = await _sender.Send(query);

        return memberResponse is not null ? Ok(memberResponse) : NotFound();
    }

    
    [HttpPost]
    public async Task<IActionResult> CreateMember([FromBody] CreateMemberCommand command)
    {
        var memberId = await _sender.Send(command);
        return CreatedAtAction(nameof(GetMemberById), new { id = memberId }, null);
    }
    [HttpPut("{id}")] // El ID viene de la ruta
    public async Task<IActionResult> UpdateMember(int id, [FromBody] UpdateMemberCommand command)
    {
        // Asegúrate de que el ID de la ruta coincida con el del cuerpo del comando
        if (id != command.Id)
        {
            return BadRequest("El ID de la ruta no coincide con el ID del cuerpo de la solicitud.");
        }

        await _sender.Send(command);
        return NoContent();
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteMember(int id)
    {
        var command = new DeleteMemberCommand(id);

        await _sender.Send(command);

        // Al igual que en la actualización, devolvemos 204 No Content
        return NoContent();
    }
    [HttpGet] // Sin parámetros, responde a la ruta base del controlador
    public async Task<IActionResult> GetAllMembers()
    {
        var query = new GetAllMembersQuery();

        var members = await _sender.Send(query);

        return Ok(members);
    }
}