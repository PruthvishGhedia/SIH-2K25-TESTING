using Dapper;
using SIH.ERP.Soap.Models;
using System.Data;
using SIH.ERP.Soap.Exceptions;

namespace SIH.ERP.Soap.Repositories;

public class GuardianRepository : RepositoryBase, IGuardianRepository
{
    public GuardianRepository(IDbConnection connection) : base(connection) { }

    public async Task<IEnumerable<Guardian>> ListAsync(int limit, int offset)
    {
        try
        {
            EnsureConnection();
            return await _connection.QueryAsync<Guardian>("SELECT * FROM guardian ORDER BY guardian_id LIMIT @limit OFFSET @offset", new { limit, offset });
        }
        catch (Exception ex)
        {
            throw new RepositoryException("Failed to list guardians", ex);
        }
    }

    public async Task<Guardian?> GetAsync(int id)
    {
        try
        {
            EnsureConnection();
            return await _connection.QueryFirstOrDefaultAsync<Guardian>("SELECT * FROM guardian WHERE \"guardian_id\"=@id", new { id });
        }
        catch (Exception ex)
        {
            throw new RepositoryException($"Failed to get guardian with ID {id}", ex);
        }
    }

    public async Task<Guardian> CreateAsync(Guardian item)
    {
        try
        {
            EnsureConnection();
            var sql = "INSERT INTO guardian(\"guardian_id\", \"student_id\", \"name\", \"relationship\", \"mobile\", \"address\") VALUES (@guardian_id, @student_id, @name, @relationship, @mobile, @address) RETURNING *";
            return await _connection.QuerySingleAsync<Guardian>(sql, item);
        }
        catch (Exception ex)
        {
            throw new RepositoryException("Failed to create guardian", ex);
        }
    }

    public async Task<Guardian?> UpdateAsync(int id, Guardian item)
    {
        try
        {
            EnsureConnection();
            var sql = "UPDATE guardian SET \"student_id\"=@student_id, \"name\"=@name, \"relationship\"=@relationship, \"mobile\"=@mobile, \"address\"=@address WHERE \"guardian_id\"=@id RETURNING *";
            return await _connection.QueryFirstOrDefaultAsync<Guardian>(sql, new { id, item.student_id, item.name, item.relationship, item.mobile, item.address });
        }
        catch (Exception ex)
        {
            throw new RepositoryException($"Failed to update guardian with ID {id}", ex);
        }
    }

    public async Task<Guardian?> RemoveAsync(int id)
    {
        try
        {
            EnsureConnection();
            var sql = "DELETE FROM guardian WHERE \"guardian_id\"=@id RETURNING *";
            return await _connection.QueryFirstOrDefaultAsync<Guardian>(sql, new { id });
        }
        catch (Exception ex)
        {
            throw new RepositoryException($"Failed to remove guardian with ID {id}", ex);
        }
    }
}