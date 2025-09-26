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
    public async Task<Role?> GetAsync(string role_id)
    {
        if (int.TryParse(role_id, out int id))
        {
            return await _repo.GetAsync(id);
        }
        return null;
    }
    
    public Task<IEnumerable<Role>> ListAsync(int limit = 100, int offset = 0) => _repo.ListAsync(limit, offset);
    
    public async Task<Role?> RemoveAsync(string role_id)
    {
        if (int.TryParse(role_id, out int id))
        {
            return await _repo.RemoveAsync(id);
        }
        return null;
    }
    
    public async Task<Role?> UpdateAsync(string role_id, Role item)
    {
        if (int.TryParse(role_id, out int id))
        {
            return await _repo.UpdateAsync(id, item);
        }
        return null;
    }
}

