using SIH.ERP.Soap.Models;
using System.Data;

namespace SIH.ERP.Soap.Repositories;

public interface IHostelRepository : IRepository
{
    Task<IEnumerable<Hostel>> ListAsync(int limit, int offset);
    Task<Hostel?> GetAsync(int id);
    Task<Hostel> CreateAsync(Hostel item);
    Task<Hostel?> UpdateAsync(int id, Hostel item);
    Task<Hostel?> RemoveAsync(int id);
}