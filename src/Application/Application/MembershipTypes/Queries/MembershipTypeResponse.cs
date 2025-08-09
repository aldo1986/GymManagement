namespace Application.MembershipTypes.Queries;

public record MembershipTypeResponse(
    int Id,
    string Name,
    string? Description,
    decimal Price,
    int DurationInDays);