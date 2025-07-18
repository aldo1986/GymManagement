using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Application.Members.Commands
{
    public sealed record DeleteMemberCommand(int Id) : IRequest;
}
