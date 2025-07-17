using MediatR;

namespace Application.Application.Members.Commands;

public sealed record UpdateMemberCommand(
    int Id, 
    string FirstName,
    string LastName,
    string Email,
    DateTime DateOfBirth,
    string PhoneNumber,
    string? Address) : IRequest; 