using Domain.Entities;
using Domain.Repositories;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories
{
    public class MemberRepository : IMemberRepository
    {
        private readonly AppDbContext _context;

        public MemberRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Member member)
        {
            await _context.Members.AddAsync(member);
            // El SaveChangesAsync se manejará en una unidad de trabajo (Unit of Work) más adelante
        }

        public async Task<IEnumerable<Member>> GetAllAsync()
        {
            return await _context.Members.ToListAsync();
        }

        public async Task<Member?> GetByIdAsync(int id)
        {
            return await _context.Members.FindAsync(id);
        }
        public async Task<bool> IsEmailUniqueAsync(string email)
        {
            // AnyAsync es muy eficiente, se detiene tan pronto como encuentra una coincidencia.
            return !await _context.Members.AnyAsync(m => m.Email == email);
        }
    }
}
