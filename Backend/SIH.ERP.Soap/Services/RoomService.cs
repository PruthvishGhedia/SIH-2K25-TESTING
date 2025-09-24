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

    public Task<Room> CreateAsync(Room item) => _repo.CreateAsync(item);
    public Task<Room?> GetAsync(int room_id) => _repo.GetAsync(room_id);
    public Task<IEnumerable<Room>> ListAsync(int limit = 100, int offset = 0) => _repo.ListAsync(limit, offset);
    public Task<Room?> RemoveAsync(int room_id) => _repo.RemoveAsync(room_id);
    public Task<Room?> UpdateAsync(int room_id, Room item) => _repo.UpdateAsync(room_id, item);
}