using SIH.ERP.Soap.Models;
using System.Data;

namespace SIH.ERP.Soap.Repositories;

public interface ISubjectRepository : IRepository
{
    Task<IEnumerable<Subject>> ListAsync(int limit, int offset);
    Task<Subject?> GetAsync(int id);
    Task<Subject> CreateAsync(Subject item);
    Task<Subject?> UpdateAsync(int id, Subject item);
    Task<Subject?> RemoveAsync(int id);
}