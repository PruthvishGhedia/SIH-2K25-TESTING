using CoreWCF;
using SIH.ERP.Soap.Contracts;
using SIH.ERP.Soap.Models;
using SIH.ERP.Soap.Repositories;

namespace SIH.ERP.Soap.Services;

public class SubjectService : ISubjectService
{
    private readonly SubjectRepository _repo;
    public SubjectService(SubjectRepository repo) { _repo = repo; }

    public Task<Subject> CreateAsync(Subject item) => _repo.CreateAsync(item);
    public Task<Subject?> GetAsync(int subject_code) => _repo.GetAsync(subject_code);
    public Task<IEnumerable<Subject>> ListAsync(int limit = 100, int offset = 0) => _repo.ListAsync(limit, offset);
    public Task<Subject?> RemoveAsync(int subject_code) => _repo.RemoveAsync(subject_code);
    public Task<Subject?> UpdateAsync(int subject_code, Subject item) => _repo.UpdateAsync(subject_code, item);
}

