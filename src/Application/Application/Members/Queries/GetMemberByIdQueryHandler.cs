using Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Application.Members.Queries
{
    public sealed class GetMemberByIdQueryHandler : IRequestHandler<GetMemberByIdQuery, MemberResponse>
    {
        private readonly IMemberRepository _memberRepository;

        public GetMemberByIdQueryHandler(IMemberRepository memberRepository)
        {
            _memberRepository = memberRepository;
        }

        public async Task<MemberResponse> Handle(GetMemberByIdQuery request, CancellationToken cancellationToken)
        {
            var member = await _memberRepository.GetByIdAsync(request.Id);

            if (member is null)
            {
                return null;
            }

            // Mapeamos la entidad del dominio al DTO de respuesta.
            var memberResponse = new MemberResponse(
                member.Id,
                $"{member.FirstName} {member.LastName}",
                member.Email,
                member.DateOfBirth,
                member.PhoneNumber,
                member.Address
            );

            return memberResponse;
        }
    }
}
