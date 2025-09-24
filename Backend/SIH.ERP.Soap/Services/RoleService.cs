using CoreWCF;
using SIH.ERP.Soap.Contracts;
using SIH.ERP.Soap.Models;
using SIH.ERP.Soap.Repositories;

namespace SIH.ERP.Soap.Services;

public class RoleService : IRoleService
{
    private readonly RoleRepository _repo;
    public RoleService(RoleRepository repo) { _repo = repo; }

    public Task<Role> CreateAsync(Role item) => _repo.CreateAsync(item);
    public Task<Role?> GetAsync(int role_id) => _repo.GetAsync(role_id);
    public Task<IEnumerable<Role>> ListAsync(int limit = 100, int offset = 0) => _repo.ListAsync(limit, offset);
    public Task<Role?> RemoveAsync(int role_id) => _repo.RemoveAsync(role_id);
    public Task<Role?> UpdateAsync(int role_id, Role item) => _repo.UpdateAsync(role_id, item);
}

