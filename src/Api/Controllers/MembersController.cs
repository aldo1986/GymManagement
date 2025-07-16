using Application.Application.Members.Commands;
using Domain.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MembersController : ControllerBase
{
    private readonly IMemberRepository _memberRepository;
    private readonly CreateMemberCommandHandler _createMemberCommandHandler;

    public MembersController(IMemberRepository memberRepository,
        CreateMemberCommandHandler createMemberCommandHandler)
    {
        memberRepository = memberRepository;
        _createMemberCommandHandler = createMemberCommandHandler;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetMemberById(int id)
    {
        var member = await _memberRepository.GetByIdAsync(id);
        if (member == null)
        {
            return NotFound();
        }
        return Ok(member);
    }
    [HttpPost]
    public async Task<IActionResult> CreateMember([FromBody] CreateMemberCommand command)
    {
        var memberId = await _createMemberCommandHandler.Handle(command);
        return CreatedAtAction(nameof(GetMemberById), new { id = memberId }, null);
    }
}
