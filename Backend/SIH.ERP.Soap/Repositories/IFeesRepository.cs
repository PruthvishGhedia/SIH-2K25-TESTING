using SIH.ERP.Soap.Models;
using System.Data;

namespace SIH.ERP.Soap.Repositories;

public interface IFeesRepository : IRepository
{
    Task<IEnumerable<Fees>> ListAsync(int limit, int offset);
    Task<Fees?> GetAsync(int id);
    Task<Fees> CreateAsync(Fees item);
    Task<Fees?> UpdateAsync(int id, Fees item);
    Task<Fees?> RemoveAsync(int id);
}