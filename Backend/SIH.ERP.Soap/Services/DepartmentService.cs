using CoreWCF;
using SIH.ERP.Soap.Contracts;
using SIH.ERP.Soap.Models;
using SIH.ERP.Soap.Repositories;

namespace SIH.ERP.Soap.Services;

public class DepartmentService : IDepartmentService
{
    private readonly DepartmentRepository _repo;
    public DepartmentService(DepartmentRepository repo)
    {
        _repo = repo;
    }

    public Task<Department> CreateAsync(Department item) => _repo.CreateAsync(item);
    public Task<Department?> GetAsync(int dept_id) => _repo.GetAsync(dept_id);
    public Task<IEnumerable<Department>> ListAsync(int limit = 100, int offset = 0) => _repo.ListAsync(limit, offset);
    public Task<Department?> RemoveAsync(int dept_id) => _repo.RemoveAsync(dept_id);
    public Task<Department?> UpdateAsync(int dept_id, Department item) => _repo.UpdateAsync(dept_id, item);
}

