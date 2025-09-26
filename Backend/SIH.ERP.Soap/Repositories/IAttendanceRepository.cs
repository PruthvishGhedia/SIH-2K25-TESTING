using SIH.ERP.Soap.Models;
using System.Data;

namespace SIH.ERP.Soap.Repositories;

public interface IAttendanceRepository : IRepository
{
    Task<IEnumerable<Attendance>> ListAsync(int limit, int offset);
    Task<Attendance?> GetAsync(int id);
    Task<Attendance> CreateAsync(Attendance item);
    Task<Attendance?> UpdateAsync(int id, Attendance item);
    Task<Attendance?> RemoveAsync(int id);
}