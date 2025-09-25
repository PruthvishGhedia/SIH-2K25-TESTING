using CoreWCF;
using SIH.ERP.Soap.Contracts;
using SIH.ERP.Soap.Models;
using SIH.ERP.Soap.Repositories;

namespace SIH.ERP.Soap.Services;

public class RoomService : IRoomService
{
    private readonly RoomRepository _repo;
    public RoomService(RoomRepository repo)
    {
        _repo = repo;
    }

    public async Task<Room> CreateAsync(Room item)
    {
        return await _repo.CreateAsync(item);
    }
    
    public async Task<Room?> GetAsync(string room_id)
    {
        if (int.TryParse(room_id, out int id))
        {
            return await _repo.GetAsync(id);
        }
        return null;
    }
    
    public Task<IEnumerable<Room>> ListAsync(int limit = 100, int offset = 0) => _repo.ListAsync(limit, offset);
    
    public async Task<Room?> RemoveAsync(string room_id)
    {
        if (int.TryParse(room_id, out int id))
        {
            return await _repo.RemoveAsync(id);
        }
        return null;
    }
    
    public async Task<Room?> UpdateAsync(string room_id, Room item)
    {
        if (int.TryParse(room_id, out int id))
        {
            return await _repo.UpdateAsync(id, item);
        }
        return null;
    }
}