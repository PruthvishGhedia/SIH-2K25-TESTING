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

    public Task<Admission> CreateAsync(Admission item) => _repo.CreateAsync(item);
    public Task<Admission?> GetAsync(int admission_id) => _repo.GetAsync(admission_id);
    public Task<IEnumerable<Admission>> ListAsync(int limit = 100, int offset = 0) => _repo.ListAsync(limit, offset);
    public Task<Admission?> RemoveAsync(int admission_id) => _repo.RemoveAsync(admission_id);
    public Task<Admission?> UpdateAsync(int admission_id, Admission item) => _repo.UpdateAsync(admission_id, item);
}