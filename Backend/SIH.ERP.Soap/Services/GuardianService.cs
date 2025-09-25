using CoreWCF;
using SIH.ERP.Soap.Contracts;
using SIH.ERP.Soap.Models;
using SIH.ERP.Soap.Repositories;

namespace SIH.ERP.Soap.Services;

public class GuardianService : IGuardianService
{
    private readonly GuardianRepository _repo;
    public GuardianService(GuardianRepository repo)
    {
        _repo = repo;
    }

    public async Task<Guardian> CreateAsync(Guardian item)
    {
        return await _repo.CreateAsync(item);
    }
    
    public async Task<Guardian?> GetAsync(string guardian_id)
    {
        if (int.TryParse(guardian_id, out int id))
        {
            return await _repo.GetAsync(id);
        }
        return null;
    }
    
    public Task<IEnumerable<Guardian>> ListAsync(int limit = 100, int offset = 0) => _repo.ListAsync(limit, offset);
    
    public async Task<Guardian?> RemoveAsync(string guardian_id)
    {
        if (int.TryParse(guardian_id, out int id))
        {
            return await _repo.RemoveAsync(id);
        }
        return null;
    }
    
    public async Task<Guardian?> UpdateAsync(string guardian_id, Guardian item)
    {
        if (int.TryParse(guardian_id, out int id))
        {
            return await _repo.UpdateAsync(id, item);
        }
        return null;
    }
}