using SIH.ERP.Soap.Models;
using System.Data;

namespace SIH.ERP.Soap.Repositories;

public interface IRoomRepository : IRepository
{
    Task<IEnumerable<Room>> ListAsync(int limit, int offset);
    Task<Room?> GetAsync(int id);
    Task<Room> CreateAsync(Room item);
    Task<Room?> UpdateAsync(int id, Room item);
    Task<Room?> RemoveAsync(int id);
}