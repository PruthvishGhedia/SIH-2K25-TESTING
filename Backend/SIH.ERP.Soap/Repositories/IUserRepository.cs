using SIH.ERP.Soap.Models;
using System.Data;

namespace SIH.ERP.Soap.Repositories;

public interface IUserRepository : IRepository
{
    Task<IEnumerable<User>> ListAsync(int limit, int offset);
    Task<User?> GetAsync(int id);
    Task<User> CreateAsync(User item);
    Task<User?> UpdateAsync(int id, User item);
    Task<User?> RemoveAsync(int id);
}