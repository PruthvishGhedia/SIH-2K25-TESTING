using Dapper;
using SIH.ERP.Soap.Models;
using System.Data;

namespace SIH.ERP.Soap.Repositories;

public class RoomRepository
{
    private readonly IDbConnection _db;
    public RoomRepository(IDbConnection db)
    {
        _db = db;
    }

    public async Task<IEnumerable<Room>> ListAsync(int limit, int offset)
    {
        return await _db.QueryAsync<Room>("SELECT * FROM room ORDER BY room_id LIMIT @limit OFFSET @offset", new { limit, offset });
    }

    public async Task<Room?> GetAsync(int id)
    {
        return await _db.QueryFirstOrDefaultAsync<Room>("SELECT * FROM room WHERE \"room_id\"=@id", new { id });
    }

    public async Task<Room> CreateAsync(Room item)
    {
        var sql = "INSERT INTO room(\"room_id\", \"hostel_id\", \"room_no\", \"capacity\", \"occupancy_status\") VALUES (@room_id, @hostel_id, @room_no, @capacity, @occupancy_status) RETURNING *";
        return await _db.QuerySingleAsync<Room>(sql, item);
    }

    public async Task<Room?> UpdateAsync(int id, Room item)
    {
        var sql = "UPDATE room SET \"hostel_id\"=@hostel_id, \"room_no\"=@room_no, \"capacity\"=@capacity, \"occupancy_status\"=@occupancy_status WHERE \"room_id\"=@id RETURNING *";
        return await _db.QueryFirstOrDefaultAsync<Room>(sql, new { id, item.hostel_id, item.room_no, item.capacity, item.occupancy_status });
    }

    public async Task<Room?> RemoveAsync(int id)
    {
        var sql = "DELETE FROM room WHERE \"room_id\"=@id RETURNING *";
        return await _db.QueryFirstOrDefaultAsync<Room>(sql, new { id });
    }
}