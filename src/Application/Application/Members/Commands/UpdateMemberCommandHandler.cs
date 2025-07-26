using Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Application.Members.Commands
{
    public sealed class UpdateMemberCommandHandler
    {
        private readonly IMemberRepository _memberRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateMemberCommandHandler(IMemberRepository memberRepository, IUnitOfWork unitOfWork)
        {
            _memberRepository = memberRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(UpdateMemberCommand request, CancellationToken cancellationToken)
        {
            
            var member = await _memberRepository.GetByIdAsync(request.Id);

            if (member is null)
            {
                
                throw new Exception($"Miembro con ID {request.Id} no encontrado.");
            }

            // 2. Actualizar las propiedades del miembro
            member.FirstName = request.FirstName;
            member.LastName = request.LastName;
            member.Email = request.Email;
            member.DateOfBirth = request.DateOfBirth;
            member.PhoneNumber = request.PhoneNumber;
            member.Address = request.Address;

            
            // 3. Guardar los cambios en la base de datos
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
