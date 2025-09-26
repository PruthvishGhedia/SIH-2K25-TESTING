using CoreWCF;
using SIH.ERP.Soap.Contracts;
using SIH.ERP.Soap.Models;
using SIH.ERP.Soap.Repositories;

namespace SIH.ERP.Soap.Services;

public class AdmissionService : IAdmissionService
{
    private readonly AdmissionRepository _repo;
    public AdmissionService(AdmissionRepository repo)
    {
        _repo = repo;
    }

    public async Task<Admission> CreateAsync(Admission item)
    {
        return await _repo.CreateAsync(item);
    }
    
    public async Task<Admission?> GetAsync(string admission_id)
    {
        if (int.TryParse(admission_id, out int id))
        {
            return await _repo.GetAsync(id);
        }
        return null;
    }
    
    public Task<IEnumerable<Admission>> ListAsync(int limit = 100, int offset = 0) => _repo.ListAsync(limit, offset);
    
    public async Task<Admission?> RemoveAsync(string admission_id)
    {
        if (int.TryParse(admission_id, out int id))
        {
            return await _repo.RemoveAsync(id);
        }
        return null;
    }
    
    public async Task<Admission?> UpdateAsync(string admission_id, Admission item)
    {
        if (int.TryParse(admission_id, out int id))
        {
            return await _repo.UpdateAsync(id, item);
        }
        return null;
    }
}