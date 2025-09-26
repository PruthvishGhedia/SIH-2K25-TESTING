using SIH.ERP.Soap.Models;
using System.Data;

namespace SIH.ERP.Soap.Repositories;

public interface IAdmissionRepository : IRepository
{
    Task<IEnumerable<Admission>> ListAsync(int limit, int offset);
    Task<Admission?> GetAsync(int id);
    Task<Admission> CreateAsync(Admission item);
    Task<Admission?> UpdateAsync(int id, Admission item);
    Task<Admission?> RemoveAsync(int id);
}