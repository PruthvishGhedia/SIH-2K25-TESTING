using CoreWCF;
using SIH.ERP.Soap.Contracts;
using SIH.ERP.Soap.Models;
using SIH.ERP.Soap.Repositories;

namespace SIH.ERP.Soap.Services;

public class FacultyService : IFacultyService
{
    private readonly IFacultyRepository _repo;
    public FacultyService(IFacultyRepository repo) { _repo = repo; }

    public Task<IEnumerable<Faculty>> ListAsync(int limit = 100, int offset = 0) => _repo.ListAsync(limit, offset);
    public Task<Faculty?> GetAsync(int faculty_id) => _repo.GetAsync(faculty_id);

    public async Task<Faculty> CreateAsync(Faculty item)
    {
        Validate(item);
        return await _repo.CreateAsync(item);
    }

    public async Task<Faculty?> UpdateAsync(int faculty_id, Faculty item)
    {
        Validate(item);
        return await _repo.UpdateAsync(faculty_id, item);
    }

    public Task<Faculty?> RemoveAsync(int faculty_id) => _repo.RemoveAsync(faculty_id);

    private void Validate(Faculty f)
    {
        if (string.IsNullOrWhiteSpace(f.first_name)) throw new FaultException("first_name is required");
        if (string.IsNullOrWhiteSpace(f.last_name)) throw new FaultException("last_name is required");
        if (string.IsNullOrWhiteSpace(f.email)) throw new FaultException("email is required");
    }
}

