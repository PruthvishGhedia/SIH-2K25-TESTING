using SIH.ERP.Soap.Models;
using System.Data;

namespace SIH.ERP.Soap.Repositories;

public interface IRoleRepository : IRepository
{
    Task<IEnumerable<Role>> ListAsync(int limit, int offset);
    Task<Role?> GetAsync(int id);
    Task<Role> CreateAsync(Role item);
    Task<Role?> UpdateAsync(int id, Role item);
    Task<Role?> RemoveAsync(int id);
}