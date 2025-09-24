using Dapper;
using SIH.ERP.Soap.Models;
using System.Data;

namespace SIH.ERP.Soap.Repositories;

public class HostelRepository
{
    private readonly IDbConnection _db;
    public HostelRepository(IDbConnection db)
    {
        _db = db;
    }

    public async Task<IEnumerable<Hostel>> ListAsync(int limit, int offset)
    {
        return await _db.QueryAsync<Hostel>("SELECT * FROM hostel ORDER BY hostel_id LIMIT @limit OFFSET @offset", new { limit, offset });
    }

    public async Task<Hostel?> GetAsync(int id)
    {
        return await _db.QueryFirstOrDefaultAsync<Hostel>("SELECT * FROM hostel WHERE \"hostel_id\"=@id", new { id });
    }

    public async Task<Hostel> CreateAsync(Hostel item)
    {
        var sql = "INSERT INTO hostel(\"hostel_id\", \"hostel_name\", \"type\") VALUES (@hostel_id, @hostel_name, @type) RETURNING *";
        return await _db.QuerySingleAsync<Hostel>(sql, item);
    }

    public async Task<Hostel?> UpdateAsync(int id, Hostel item)
    {
        var sql = "UPDATE hostel SET \"hostel_name\"=@hostel_name, \"type\"=@type WHERE \"hostel_id\"=@id RETURNING *";
        return await _db.QueryFirstOrDefaultAsync<Hostel>(sql, new { id, item.hostel_name, item.type });
    }

    public async Task<Hostel?> RemoveAsync(int id)
    {
        var sql = "DELETE FROM hostel WHERE \"hostel_id\"=@id RETURNING *";
        return await _db.QueryFirstOrDefaultAsync<Hostel>(sql, new { id });
    }
}