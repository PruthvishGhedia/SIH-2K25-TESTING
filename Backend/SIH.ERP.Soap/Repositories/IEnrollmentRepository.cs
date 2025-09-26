using SIH.ERP.Soap.Models;
using System.Data;

namespace SIH.ERP.Soap.Repositories;

public interface IEnrollmentRepository : IRepository
{
    Task<IEnumerable<Enrollment>> ListAsync(int limit, int offset);
    Task<Enrollment?> GetAsync(int id);
    Task<Enrollment> CreateAsync(Enrollment item);
    Task<Enrollment?> UpdateAsync(int id, Enrollment item);
    Task<Enrollment?> RemoveAsync(int id);
}