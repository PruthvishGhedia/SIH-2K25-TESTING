using CoreWCF;
using SIH.ERP.Soap.Contracts;
using SIH.ERP.Soap.Models;
using SIH.ERP.Soap.Repositories;

namespace SIH.ERP.Soap.Services;

public class UserRoleService : IUserRoleService
{
    private readonly UserRoleRepository _repo;
    public UserRoleService(UserRoleRepository repo)
    {
        _repo = repo;
    }

    public Task<UserRole> CreateAsync(UserRole item) => _repo.CreateAsync(item);
    public Task<UserRole?> GetAsync(int user_role_id) => _repo.GetAsync(user_role_id);
    public Task<IEnumerable<UserRole>> ListAsync(int limit = 100, int offset = 0) => _repo.ListAsync(limit, offset);
    public Task<UserRole?> RemoveAsync(int user_role_id) => _repo.RemoveAsync(user_role_id);
    public Task<UserRole?> UpdateAsync(int user_role_id, UserRole item) => _repo.UpdateAsync(user_role_id, item);
}