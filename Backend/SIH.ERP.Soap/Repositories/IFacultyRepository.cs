using SIH.ERP.Soap.Models;
using System.Data;

namespace SIH.ERP.Soap.Repositories;

public interface IFacultyRepository : IRepository
{
    Task<IEnumerable<Faculty>> ListAsync(int limit, int offset);
    Task<Faculty?> GetAsync(int id);
    Task<Faculty> CreateAsync(Faculty item);
    Task<Faculty?> UpdateAsync(int id, Faculty item);
    Task<Faculty?> RemoveAsync(int id);
}