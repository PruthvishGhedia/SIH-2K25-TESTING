using Dapper;
using SIH.ERP.Soap.Models;
using System.Data;

namespace SIH.ERP.Soap.Repositories;

public class GuardianRepository
{
    private readonly IDbConnection _db;
    public GuardianRepository(IDbConnection db)
    {
        _db = db;
    }

    public async Task<IEnumerable<Guardian>> ListAsync(int limit, int offset)
    {
        return await _db.QueryAsync<Guardian>("SELECT * FROM guardian ORDER BY guardian_id LIMIT @limit OFFSET @offset", new { limit, offset });
    }

    public async Task<Guardian?> GetAsync(int id)
    {
        return await _db.QueryFirstOrDefaultAsync<Guardian>("SELECT * FROM guardian WHERE \"guardian_id\"=@id", new { id });
    }

    public async Task<Guardian> CreateAsync(Guardian item)
    {
        var sql = "INSERT INTO guardian(\"guardian_id\", \"student_id\", \"name\", \"relationship\", \"mobile\", \"address\") VALUES (@guardian_id, @student_id, @name, @relationship, @mobile, @address) RETURNING *";
        return await _db.QuerySingleAsync<Guardian>(sql, item);
    }

    public async Task<Guardian?> UpdateAsync(int id, Guardian item)
    {
        var sql = "UPDATE guardian SET \"student_id\"=@student_id, \"name\"=@name, \"relationship\"=@relationship, \"mobile\"=@mobile, \"address\"=@address WHERE \"guardian_id\"=@id RETURNING *";
        return await _db.QueryFirstOrDefaultAsync<Guardian>(sql, new { id, item.student_id, item.name, item.relationship, item.mobile, item.address });
    }

    public async Task<Guardian?> RemoveAsync(int id)
    {
        var sql = "DELETE FROM guardian WHERE \"guardian_id\"=@id RETURNING *";
        return await _db.QueryFirstOrDefaultAsync<Guardian>(sql, new { id });
    }
}