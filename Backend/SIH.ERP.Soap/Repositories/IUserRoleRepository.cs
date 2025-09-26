using SIH.ERP.Soap.Models;
using System.Data;

namespace SIH.ERP.Soap.Repositories;

public interface IUserRoleRepository : IRepository
{
    Task<IEnumerable<UserRole>> ListAsync(int limit, int offset);
    Task<UserRole?> GetAsync(int id);
    Task<UserRole> CreateAsync(UserRole item);
    Task<UserRole?> UpdateAsync(int id, UserRole item);
    Task<UserRole?> RemoveAsync(int id);
}