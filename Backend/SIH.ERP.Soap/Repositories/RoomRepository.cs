using Dapper;
using SIH.ERP.Soap.Models;
using System.Data;
using SIH.ERP.Soap.Exceptions;

namespace SIH.ERP.Soap.Repositories;

public class RoomRepository : RepositoryBase, IRoomRepository
{
    public RoomRepository(IDbConnection connection) : base(connection) { }

    public async Task<IEnumerable<Room>> ListAsync(int limit, int offset)
    {
        try
        {
            EnsureConnection();
            return await _connection.QueryAsync<Room>("SELECT * FROM room ORDER BY room_id LIMIT @limit OFFSET @offset", new { limit, offset });
        }
        catch (Exception ex)
        {
            throw new RepositoryException("Failed to list rooms", ex);
        }
    }

    public async Task<Room?> GetAsync(int id)
    {
        try
        {
            EnsureConnection();
            return await _connection.QueryFirstOrDefaultAsync<Room>("SELECT * FROM room WHERE \"room_id\"=@id", new { id });
        }
        catch (Exception ex)
        {
            throw new RepositoryException($"Failed to get room with ID {id}", ex);
        }
    }

    public async Task<Room> CreateAsync(Room item)
    {
        try
        {
            EnsureConnection();
            var sql = "INSERT INTO room(\"room_id\", \"hostel_id\", \"room_no\", \"capacity\", \"occupancy_status\") VALUES (@room_id, @hostel_id, @room_no, @capacity, @occupancy_status) RETURNING *";
            return await _connection.QuerySingleAsync<Room>(sql, item);
        }
        catch (Exception ex)
        {
            throw new RepositoryException("Failed to create room", ex);
        }
    }

    public async Task<Room?> UpdateAsync(int id, Room item)
    {
        try
        {
            EnsureConnection();
            var sql = "UPDATE room SET \"hostel_id\"=@hostel_id, \"room_no\"=@room_no, \"capacity\"=@capacity, \"occupancy_status\"=@occupancy_status WHERE \"room_id\"=@id RETURNING *";
            return await _connection.QueryFirstOrDefaultAsync<Room>(sql, new { id, item.hostel_id, item.room_no, item.capacity, item.occupancy_status });
        }
        catch (Exception ex)
        {
            throw new RepositoryException($"Failed to update room with ID {id}", ex);
        }
    }

    public async Task<Room?> RemoveAsync(int id)
    {
        try
        {
            EnsureConnection();
            var sql = "DELETE FROM room WHERE \"room_id\"=@id RETURNING *";
            return await _connection.QueryFirstOrDefaultAsync<Room>(sql, new { id });
        }
        catch (Exception ex)
        {
            throw new RepositoryException($"Failed to remove room with ID {id}", ex);
        }
    }
}