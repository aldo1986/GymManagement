using Domain.Repositories;
using MediatR;

namespace Application.MembershipTypes.Queries;

public class GetAllMembershipTypesQueryHandler
    : IRequestHandler<GetAllMembershipTypesQuery, IEnumerable<MembershipTypeResponse>>
{
    private readonly IMembershipTypeRepository _membershipTypeRepository;

    public GetAllMembershipTypesQueryHandler(IMembershipTypeRepository membershipTypeRepository)
    {
        // Necesitaremos añadir GetAllAsync() a nuestro repositorio
        _membershipTypeRepository = membershipTypeRepository;
    }

    public async Task<IEnumerable<MembershipTypeResponse>> Handle(
        GetAllMembershipTypesQuery request,
        CancellationToken cancellationToken)
    {
        var membershipTypes = await _membershipTypeRepository.GetAllAsync();

        return membershipTypes.Select(mt => new MembershipTypeResponse(
            mt.Id,
            mt.Name,
            mt.Description,
            mt.Price,
            mt.DurationInDays
        ));
    }
}