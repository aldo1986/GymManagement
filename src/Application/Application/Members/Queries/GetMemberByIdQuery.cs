using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Application.Members.Queries
{
    public sealed record GetMemberByIdQuery(int Id) : IRequest<MemberResponse>;
}
