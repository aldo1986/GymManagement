using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public interface IMemberRepository
    {
        Task<Member?> GetByIdAsync(int id);
        Task<IEnumerable<Member>> GetAllAsync();
        Task AddAsync(Member member);
        //Task DeleteAsync(int id);
        //Task UpdateAsync(Member member);
    }
}
