using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public interface IMembershipTypeRepository
    {
        Task AddAsync(MembershipType membershipType);
        Task<MembershipType?> GetByIdAsync(int id);
        Task<IEnumerable<MembershipType>> GetAllAsync();
    }
}
