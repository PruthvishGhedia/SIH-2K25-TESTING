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

    public Task<Guardian> CreateAsync(Guardian item) => _repo.CreateAsync(item);
    public Task<Guardian?> GetAsync(int guardian_id) => _repo.GetAsync(guardian_id);
    public Task<IEnumerable<Guardian>> ListAsync(int limit = 100, int offset = 0) => _repo.ListAsync(limit, offset);
    public Task<Guardian?> RemoveAsync(int guardian_id) => _repo.RemoveAsync(guardian_id);
    public Task<Guardian?> UpdateAsync(int guardian_id, Guardian item) => _repo.UpdateAsync(guardian_id, item);
}