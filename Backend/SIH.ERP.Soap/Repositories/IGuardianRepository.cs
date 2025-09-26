using SIH.ERP.Soap.Models;
using System.Data;

namespace SIH.ERP.Soap.Repositories;

public interface IGuardianRepository : IRepository
{
    Task<IEnumerable<Guardian>> ListAsync(int limit, int offset);
    Task<Guardian?> GetAsync(int id);
    Task<Guardian> CreateAsync(Guardian item);
    Task<Guardian?> UpdateAsync(int id, Guardian item);
    Task<Guardian?> RemoveAsync(int id);
}