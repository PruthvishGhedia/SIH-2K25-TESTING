using Dapper;
using SIH.ERP.Soap.Models;
using System.Data;
using SIH.ERP.Soap.Exceptions;

namespace SIH.ERP.Soap.Repositories;

public class FeesRepository : RepositoryBase, IFeesRepository
{
    public FeesRepository(IDbConnection connection) : base(connection) { }

    public async Task<IEnumerable<Fees>> ListAsync(int limit, int offset)
    {
        try
        {
            EnsureConnection();
            return await _connection.QueryAsync<Fees>("SELECT * FROM fees ORDER BY fee_id LIMIT @limit OFFSET @offset", new { limit, offset });
        }
        catch (Exception ex)
        {
            throw new RepositoryException("Failed to list fees", ex);
        }
    }

    public async Task<Fees?> GetAsync(int id)
    {
        try
        {
            EnsureConnection();
            return await _connection.QueryFirstOrDefaultAsync<Fees>("SELECT * FROM fees WHERE \"fee_id\"=@id", new { id });
        }
        catch (Exception ex)
        {
            throw new RepositoryException($"Failed to get fee with ID {id}", ex);
        }
    }

    public async Task<Fees> CreateAsync(Fees item)
    {
        try
        {
            EnsureConnection();
            var sql = "INSERT INTO fees(\"fee_id\", \"student_id\", \"fee_type\", \"amount\", \"due_date\", \"paid_on\", \"payment_status\", \"payment_mode\", \"created_at\", \"updated_at\") VALUES (@fee_id, @student_id, @fee_type, @amount, @due_date, @paid_on, @payment_status, @payment_mode, @created_at, @updated_at) RETURNING *";
            return await _connection.QuerySingleAsync<Fees>(sql, item);
        }
        catch (Exception ex)
        {
            throw new RepositoryException("Failed to create fee", ex);
        }
    }

    public async Task<Fees?> UpdateAsync(int id, Fees item)
    {
        try
        {
            EnsureConnection();
            var sql = "UPDATE fees SET \"student_id\"=@student_id, \"fee_type\"=@fee_type, \"amount\"=@amount, \"due_date\"=@due_date, \"paid_on\"=@paid_on, \"payment_status\"=@payment_status, \"payment_mode\"=@payment_mode, \"updated_at\"=@updated_at WHERE \"fee_id\"=@id RETURNING *";
            return await _connection.QueryFirstOrDefaultAsync<Fees>(sql, new { id, item.student_id, item.fee_type, item.amount, item.due_date, item.paid_on, item.payment_status, item.payment_mode, item.updated_at });
        }
        catch (Exception ex)
        {
            throw new RepositoryException($"Failed to update fee with ID {id}", ex);
        }
    }

    public async Task<Fees?> RemoveAsync(int id)
    {
        try
        {
            EnsureConnection();
            var sql = "DELETE FROM fees WHERE \"fee_id\"=@id RETURNING *";
            return await _connection.QueryFirstOrDefaultAsync<Fees>(sql, new { id });
        }
        catch (Exception ex)
        {
            throw new RepositoryException($"Failed to remove fee with ID {id}", ex);
        }
    }
}