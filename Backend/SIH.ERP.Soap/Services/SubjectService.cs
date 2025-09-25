using CoreWCF;
using SIH.ERP.Soap.Contracts;
using SIH.ERP.Soap.Models;
using SIH.ERP.Soap.Repositories;

namespace SIH.ERP.Soap.Services;

public class SubjectService : ISubjectService
{
    private readonly SubjectRepository _repo;
    public SubjectService(SubjectRepository repo) { _repo = repo; }

    public async Task<Subject> CreateAsync(Subject item)
    {
        return await _repo.CreateAsync(item);
    }
    
    public async Task<Subject?> GetAsync(string subject_code)
    {
        if (int.TryParse(subject_code, out int code))
        {
            return await _repo.GetAsync(code);
        }
        return null;
    }
    
    public Task<IEnumerable<Subject>> ListAsync(int limit = 100, int offset = 0) => _repo.ListAsync(limit, offset);
    
    public async Task<Subject?> RemoveAsync(string subject_code)
    {
        if (int.TryParse(subject_code, out int code))
        {
            return await _repo.RemoveAsync(code);
        }
        return null;
    }
    
    public async Task<Subject?> UpdateAsync(string subject_code, Subject item)
    {
        if (int.TryParse(subject_code, out int code))
        {
            return await _repo.UpdateAsync(code, item);
        }
        return null;
    }
}

