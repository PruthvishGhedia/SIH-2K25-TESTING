using SIH.ERP.Soap.Models;
using System.Data;

namespace SIH.ERP.Soap.Repositories;

public interface IResultRepository : IRepository
{
    Task<IEnumerable<Result>> ListAsync(int limit, int offset);
    Task<Result?> GetAsync(int id);
    Task<Result> CreateAsync(Result item);
    Task<Result?> UpdateAsync(int id, Result item);
    Task<Result?> RemoveAsync(int id);
}