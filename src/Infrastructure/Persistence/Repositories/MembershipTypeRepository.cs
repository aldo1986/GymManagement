using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class MembershipTypeRepository : IMembershipTypeRepository
{
    private readonly AppDbContext _context;

    public MembershipTypeRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(MembershipType membershipType)
    {
        await _context.MembershipTypes.AddAsync(membershipType);
    }

    public async Task<MembershipType?> GetByIdAsync(int id)
    {
        return await _context.MembershipTypes.FindAsync(id);
    }
}