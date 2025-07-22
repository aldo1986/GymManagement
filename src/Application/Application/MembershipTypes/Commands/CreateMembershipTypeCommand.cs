using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Application.MembershipTypes.Commands
{
    public sealed record CreateMembershipTypeCommand(
         string Name,
         string Description,
         decimal Price,
         int DurationInDays
        ) : IRequest<int>;
}
