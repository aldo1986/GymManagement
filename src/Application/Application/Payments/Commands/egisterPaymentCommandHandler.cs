using Application.Application.Payments.Commands;
using Domain.Entities;
using Domain.Repositories;
using MediatR;

namespace Application.Payments.Commands;

public sealed class RegisterPaymentCommandHandler
    : IRequestHandler<RegisterPaymentCommand, int>
{
    private readonly IMemberRepository _memberRepository;
    private readonly IMembershipTypeRepository _membershipTypeRepository;
    private readonly IPaymentRepository _paymentRepository;
    private readonly IUnitOfWork _unitOfWork;

    public RegisterPaymentCommandHandler(
        IMemberRepository memberRepository,
        IMembershipTypeRepository membershipTypeRepository,
        IPaymentRepository paymentRepository,
        IUnitOfWork unitOfWork)
    {
        _memberRepository = memberRepository;
        _membershipTypeRepository = membershipTypeRepository;
        _paymentRepository = paymentRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<int> Handle(
        RegisterPaymentCommand request,
        CancellationToken cancellationToken)
    {
        // 1. Validar que el tipo de membresía existe
        var membershipType = await _membershipTypeRepository.GetByIdAsync(request.MembershipTypeId);
        if (membershipType is null)
        {
            throw new Exception($"El tipo de membresía con ID {request.MembershipTypeId} no fue encontrado.");
        }

        // 2. Validar que el miembro existe
        var member = await _memberRepository.GetByIdAsync(request.MemberId);
        if (member is null)
        {
            throw new Exception($"El miembro con ID {request.MemberId} no fue encontrado.");
        }

        // 3. Crear la entidad de pago
        var payment = new Payment
        {
            MemberId = request.MemberId,
            MembershipTypeId = request.MembershipTypeId,
            Amount = membershipType.Price, // El precio viene del tipo de membresía
            PaymentDate = DateTime.UtcNow
        };

        await _paymentRepository.AddAsync(payment);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return payment.Id;
    }
}