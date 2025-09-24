using Dapper;
using SIH.ERP.Soap.Models;
using SIH.ERP.Soap.Exceptions;
using System.Data;

namespace SIH.ERP.Soap.Repositories;

public interface IFacultyRepository
{
    Task<IEnumerable<Faculty>> ListAsync(int limit, int offset);
    Task<Faculty?> GetAsync(int id);
    Task<Faculty> CreateAsync(Faculty item);
    Task<Faculty?> UpdateAsync(int id, Faculty item);
    Task<Faculty?> RemoveAsync(int id);
}

public class FacultyRepository : RepositoryBase, IFacultyRepository
{
    public FacultyRepository(IDbConnection connection) : base(connection) { }

    public async Task<IEnumerable<Faculty>> ListAsync(int limit, int offset)
    {
        try
        {
            EnsureConnection();
            var sql = "SELECT * FROM faculty ORDER BY faculty_id LIMIT @limit OFFSET @offset";
            return await _connection.QueryAsync<Faculty>(sql, new { limit, offset });
        }
        catch (Exception ex)
        {
            throw new RepositoryException("Failed to list faculty", ex);
        }
    }

    public async Task<Faculty?> GetAsync(int id)
    {
        try
        {
            EnsureConnection();
            var sql = "SELECT * FROM faculty WHERE faculty_id = @id";
            return await _connection.QueryFirstOrDefaultAsync<Faculty>(sql, new { id });
        }
        catch (Exception ex)
        {
            throw new RepositoryException($"Failed to get faculty with ID {id}", ex);
        }
    }

    public async Task<Faculty> CreateAsync(Faculty item)
    {
        try
        {
            EnsureConnection();
            var sql = @"INSERT INTO faculty (first_name, last_name, email, phone, department_id, is_active) 
                        VALUES (@first_name, @last_name, @email, @phone, @department_id, @is_active)
                        RETURNING *";
            return await _connection.QuerySingleAsync<Faculty>(sql, item);
        }
        catch (Exception ex)
        {
            throw new RepositoryException("Failed to create faculty", ex);
        }
    }

    public async Task<Faculty?> UpdateAsync(int id, Faculty item)
    {
        try
        {
            EnsureConnection();
            var sql = @"UPDATE faculty SET first_name=@first_name, last_name=@last_name, email=@email, phone=@phone, 
                        department_id=@department_id, is_active=@is_active WHERE faculty_id=@id RETURNING *";
            return await _connection.QueryFirstOrDefaultAsync<Faculty>(sql, new { id, item.first_name, item.last_name, item.email, item.phone, item.department_id, item.is_active });
        }
        catch (Exception ex)
        {
            throw new RepositoryException($"Failed to update faculty with ID {id}", ex);
        }
    }

    public async Task<Faculty?> RemoveAsync(int id)
    {
        try
        {
            EnsureConnection();
            var sql = "DELETE FROM faculty WHERE faculty_id=@id RETURNING *";
            return await _connection.QueryFirstOrDefaultAsync<Faculty>(sql, new { id });
        }
        catch (Exception ex)
        {
            throw new RepositoryException($"Failed to remove faculty with ID {id}", ex);
        }
    }
}

