using MediatR;

namespace Application.MembershipTypes.Queries;

public record GetAllMembershipTypesQuery() : IRequest<IEnumerable<MembershipTypeResponse>>;