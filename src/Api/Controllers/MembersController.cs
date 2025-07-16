using Domain.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MembersController : ControllerBase
{
    private readonly IMemberRepository _memberRepository;

    public MembersController(IMemberRepository memberRepository)
    {
        _memberRepository = memberRepository;
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
}
