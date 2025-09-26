using Dapper;
using SIH.ERP.Soap.Models;
using SIH.ERP.Soap.Exceptions;
using System.Data;

namespace SIH.ERP.Soap.Repositories;

public interface IPaymentRepository
{
    Task<IEnumerable<Payment>> ListAsync(int limit, int offset);
    Task<Payment?> GetAsync(int id);
    Task<Payment> CreateAsync(Payment item);
    Task<Payment?> UpdateAsync(int id, Payment item);
    Task<Payment?> RemoveAsync(int id);
}

public class PaymentRepository : RepositoryBase, IPaymentRepository
{
    public PaymentRepository(IDbConnection connection) : base(connection) { }

    public async Task<IEnumerable<Payment>> ListAsync(int limit, int offset)
    {
        try
        {
            EnsureConnection();
            var sql = "SELECT * FROM payment ORDER BY payment_id LIMIT @limit OFFSET @offset";
            return await _connection.QueryAsync<Payment>(sql, new { limit, offset });
        }
        catch (Exception ex)
        {
            throw new RepositoryException("Failed to list payments", ex);
        }
    }

    public async Task<Payment?> GetAsync(int id)
    {
        try
        {
            EnsureConnection();
            var sql = "SELECT * FROM payment WHERE payment_id = @id";
            return await _connection.QueryFirstOrDefaultAsync<Payment>(sql, new { id });
        }
        catch (Exception ex)
        {
            throw new RepositoryException($"Failed to get payment with ID {id}", ex);
        }
    }

    public async Task<Payment> CreateAsync(Payment item)
    {
        try
        {
            EnsureConnection();
            var sql = @"INSERT INTO payment (student_id, amount, payment_date, status, mode)
                        VALUES (@student_id, @amount, @payment_date, @status, @mode) RETURNING *";
            return await _connection.QuerySingleAsync<Payment>(sql, item);
        }
        catch (Exception ex)
        {
            throw new RepositoryException("Failed to create payment", ex);
        }
    }

    public async Task<Payment?> UpdateAsync(int id, Payment item)
    {
        try
        {
            EnsureConnection();
            var sql = @"UPDATE payment SET student_id=@student_id, amount=@amount, payment_date=@payment_date, status=@status, mode=@mode
                        WHERE payment_id=@id RETURNING *";
            return await _connection.QueryFirstOrDefaultAsync<Payment>(sql, new { id, item.student_id, item.amount, item.payment_date, item.status, item.mode });
        }
        catch (Exception ex)
        {
            throw new RepositoryException($"Failed to update payment with ID {id}", ex);
        }
    }

    public async Task<Payment?> RemoveAsync(int id)
    {
        try
        {
            EnsureConnection();
            var sql = "DELETE FROM payment WHERE payment_id=@id RETURNING *";
            return await _connection.QueryFirstOrDefaultAsync<Payment>(sql, new { id });
        }
        catch (Exception ex)
        {
            throw new RepositoryException($"Failed to remove payment with ID {id}", ex);
        }
    }
}

