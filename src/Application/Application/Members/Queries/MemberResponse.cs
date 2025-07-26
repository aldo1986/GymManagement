using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Application.Members.Queries
{
   public sealed record MemberResponse(
       int Id,
       string FullName,
       string Email,
       DateTime DateOfBirth,
       string PhoneNumber,
       string? Address
       );
}
