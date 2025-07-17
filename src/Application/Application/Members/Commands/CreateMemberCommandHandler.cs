using Domain.Entities;
using Domain.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Application.Members.Commands
{
    public sealed class CreateMemberCommandHandler
    {
        private readonly IMemberRepository _memberRepository;
        private readonly IUnitOfWork _unitOfWork;
        public CreateMemberCommandHandler(IMemberRepository memberRepository, IUnitOfWork unitOfWork)
        {
            _memberRepository = memberRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<int> Handle(CreateMemberCommand command)
        {
            if (!await _memberRepository.IsEmailUniqueAsync(command.Email))
            {
                throw new ValidationException("El correo electrónico ya está en uso.");
            }

            var member = new Member
            {
                FirstName = command.FirstName,
                LastName = command.LastName,
                Email = command.Email,
                MemberSince = DateTime.UtcNow,
                DateOfBirth = command.DateOfBirth,
                PhoneNumber = command.PhoneNumber,
                Address = command.Address
            };

            await _memberRepository.AddAsync(member);
            await _unitOfWork.SaveChangesAsync();
            return member.Id;
        }
    }
}
