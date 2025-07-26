using MediatR;

namespace Application.Application.Payments.Commands;

public sealed record RegisterPaymentCommand(
    int MemberId,
    int MembershipTypeId) : IRequest<int>;