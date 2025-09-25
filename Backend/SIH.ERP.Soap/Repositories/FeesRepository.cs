using Dapper;
using SIH.ERP.Soap.Models;
using System.Data;

namespace SIH.ERP.Soap.Repositories;

public interface IFeesRepository
{
    Task<IEnumerable<Fees>> ListAsync(int limit, int offset);
    Task<Fees?> GetAsync(int id);
    Task<Fees> CreateAsync(Fees item);
    Task<Fees?> UpdateAsync(int id, Fees item);
    Task<Fees?> RemoveAsync(int id);
}

public class FeesRepository : IFeesRepository
{
    private readonly IDbConnection _db;
    public FeesRepository(IDbConnection db)
    {
        _db = db;
    }

    public async Task<IEnumerable<Fees>> ListAsync(int limit, int offset)
    {
        return await _db.QueryAsync<Fees>("SELECT * FROM fees ORDER BY fee_id LIMIT @limit OFFSET @offset", new { limit, offset });
    }

    public async Task<Fees?> GetAsync(int id)
    {
        return await _db.QueryFirstOrDefaultAsync<Fees>("SELECT * FROM fees WHERE \"fee_id\"=@id", new { id });
    }

    public async Task<Fees> CreateAsync(Fees item)
    {
        var sql = "INSERT INTO fees(\"fee_id\", \"student_id\", \"fee_type\", \"amount\", \"due_date\", \"paid_on\", \"payment_status\", \"payment_mode\", \"created_at\", \"updated_at\") VALUES (@fee_id, @student_id, @fee_type, @amount, @due_date, @paid_on, @payment_status, @payment_mode, @created_at, @updated_at) RETURNING *";
        return await _db.QuerySingleAsync<Fees>(sql, item);
    }

    public async Task<Fees?> UpdateAsync(int id, Fees item)
    {
        var sql = "UPDATE fees SET \"student_id\"=@student_id, \"fee_type\"=@fee_type, \"amount\"=@amount, \"due_date\"=@due_date, \"paid_on\"=@paid_on, \"payment_status\"=@payment_status, \"payment_mode\"=@payment_mode, \"updated_at\"=@updated_at WHERE \"fee_id\"=@id RETURNING *";
        return await _db.QueryFirstOrDefaultAsync<Fees>(sql, new { id, item.student_id, item.fee_type, item.amount, item.due_date, item.paid_on, item.payment_status, item.payment_mode, item.updated_at });
    }

    public async Task<Fees?> RemoveAsync(int id)
    {
        var sql = "DELETE FROM fees WHERE \"fee_id\"=@id RETURNING *";
        return await _db.QueryFirstOrDefaultAsync<Fees>(sql, new { id });
    }
}