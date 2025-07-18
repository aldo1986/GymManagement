using Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Application.Members.Queries
{
    public sealed class GetAllMembersQueryHandler
    : IRequestHandler<GetAllMembersQuery, IEnumerable<MemberResponse>>
    {
        private readonly IMemberRepository _memberRepository;

        public GetAllMembersQueryHandler(IMemberRepository memberRepository)
        {
            _memberRepository = memberRepository;
        }

        public async Task<IEnumerable<MemberResponse>> Handle(
            GetAllMembersQuery request,
            CancellationToken cancellationToken)
        {
            var members = await _memberRepository.GetAllAsync();

            // Mapeamos la lista de entidades a una lista de DTOs
            return members.Select(member => new MemberResponse(
                member.Id,
                $"{member.FirstName} {member.LastName}",
                member.Email,
                member.DateOfBirth,
                member.PhoneNumber,
                member.Address
            ));
        }
    }
}
