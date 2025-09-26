using Dapper;
using SIH.ERP.Soap.Models;
using System.Data;

namespace SIH.ERP.Soap.Repositories;

public class ResultRepository
{
    private readonly IDbConnection _db;
    public ResultRepository(IDbConnection db)
    {
        _db = db;
    }

    public async Task<IEnumerable<Result>> ListAsync(int limit, int offset)
    {
        return await _db.QueryAsync<Result>("SELECT * FROM result ORDER BY result_id LIMIT @limit OFFSET @offset", new { limit, offset });
    }

    public async Task<Result?> GetAsync(int id)
    {
        return await _db.QueryFirstOrDefaultAsync<Result>("SELECT * FROM result WHERE \"result_id\"=@id", new { id });
    }

    public async Task<Result> CreateAsync(Result item)
    {
        var sql = "INSERT INTO result(\"result_id\", \"exam_id\", \"student_id\", \"marks\", \"grade\") VALUES (@result_id, @exam_id, @student_id, @marks, @grade) RETURNING *";
        return await _db.QuerySingleAsync<Result>(sql, item);
    }

    public async Task<Result?> UpdateAsync(int id, Result item)
    {
        var sql = "UPDATE result SET \"exam_id\"=@exam_id, \"student_id\"=@student_id, \"marks\"=@marks, \"grade\"=@grade WHERE \"result_id\"=@id RETURNING *";
        return await _db.QueryFirstOrDefaultAsync<Result>(sql, new { id, item.exam_id, item.student_id, item.marks, item.grade });
    }

    public async Task<Result?> RemoveAsync(int id)
    {
        var sql = "DELETE FROM result WHERE \"result_id\"=@id RETURNING *";
        return await _db.QueryFirstOrDefaultAsync<Result>(sql, new { id });
    }
}