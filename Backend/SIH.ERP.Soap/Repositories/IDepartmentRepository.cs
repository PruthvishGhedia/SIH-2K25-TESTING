using SIH.ERP.Soap.Models;
using System.Data;

namespace SIH.ERP.Soap.Repositories;

public interface IDepartmentRepository : IRepository
{
    Task<IEnumerable<Department>> ListAsync(int limit, int offset);
    Task<Department?> GetAsync(int id);
    Task<Department> CreateAsync(Department item);
    Task<Department?> UpdateAsync(int id, Department item);
    Task<Department?> RemoveAsync(int id);
}