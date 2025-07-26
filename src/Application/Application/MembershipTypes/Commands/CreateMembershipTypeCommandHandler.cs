using Domain.Entities;
using Domain.Repositories;
using MediatR;

namespace Application.Application.MembershipTypes.Commands;

public sealed class CreateMembershipTypeCommandHandler
    : IRequestHandler<CreateMembershipTypeCommand, int>
{
    private readonly IMembershipTypeRepository _membershipTypeRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateMembershipTypeCommandHandler(
        IMembershipTypeRepository membershipTypeRepository,
        IUnitOfWork unitOfWork)
    {
        _membershipTypeRepository = membershipTypeRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<int> Handle(
        CreateMembershipTypeCommand request,
        CancellationToken cancellationToken)
    {
        var membershipType = new MembershipType
        {
            Name = request.Name,
            Description = request.Description,
            Price = request.Price,
            DurationInDays = request.DurationInDays
        };

        await _membershipTypeRepository.AddAsync(membershipType);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return membershipType.Id;
    }
}