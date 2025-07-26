using Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Application.Members.Commands
{
    public sealed class DeleteMemberCommandHandler : IRequestHandler<DeleteMemberCommand>
    {
        private readonly IMemberRepository _memberRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteMemberCommandHandler(IMemberRepository memberRepository, IUnitOfWork unitOfWork)
        {
            _memberRepository = memberRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(DeleteMemberCommand request, CancellationToken cancellationToken)
        {
            // 1. Buscar el miembro por su ID
            var member = await _memberRepository.GetByIdAsync(request.Id);

            if (member is null)
            {
                // Si no se encuentra, no hacemos nada o lanzamos una excepción.
                // En este caso, no hacer nada es seguro (la entidad ya no existe).
                return;
            }

            // 2. Marcar la entidad para ser eliminada
            _memberRepository.Delete(member);

            // 3. Guardar los cambios en la base de datos
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
