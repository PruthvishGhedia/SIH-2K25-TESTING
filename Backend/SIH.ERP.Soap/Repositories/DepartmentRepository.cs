using Dapper;
using SIH.ERP.Soap.Models;
using System.Data;
using SIH.ERP.Soap.Exceptions;

namespace SIH.ERP.Soap.Repositories;

public class DepartmentRepository : RepositoryBase, IDepartmentRepository
{
    public DepartmentRepository(IDbConnection connection) : base(connection) { }

    public async Task<IEnumerable<Department>> ListAsync(int limit, int offset)
    {
        try
        {
            EnsureConnection();
            return await _connection.QueryAsync<Department>("SELECT * FROM department ORDER BY dept_id LIMIT @limit OFFSET @offset", new { limit, offset });
        }
        catch (Exception ex)
        {
            throw new RepositoryException("Failed to list departments", ex);
        }
    }

    public async Task<Department?> GetAsync(int id)
    {
        try
        {
            EnsureConnection();
            return await _connection.QueryFirstOrDefaultAsync<Department>("SELECT * FROM department WHERE \"dept_id\"=@id", new { id });
        }
        catch (Exception ex)
        {
            throw new RepositoryException($"Failed to get department with ID {id}", ex);
        }
    }

    public async Task<Department> CreateAsync(Department item)
    {
        try
        {
            EnsureConnection();
            var sql = "INSERT INTO department(\"dept_id\", \"dept_name\") VALUES (@dept_id, @dept_name) RETURNING *";
            return await _connection.QuerySingleAsync<Department>(sql, item);
        }
        catch (Exception ex)
        {
            throw new RepositoryException("Failed to create department", ex);
        }
    }

    public async Task<Department?> UpdateAsync(int id, Department item)
    {
        try
        {
            EnsureConnection();
            var sql = "UPDATE department SET \"dept_name\"=@dept_name WHERE \"dept_id\"=@id RETURNING *";
            return await _connection.QueryFirstOrDefaultAsync<Department>(sql, new { id, item.dept_name });
        }
        catch (Exception ex)
        {
            throw new RepositoryException($"Failed to update department with ID {id}", ex);
        }
    }

    public async Task<Department?> RemoveAsync(int id)
    {
        try
        {
            EnsureConnection();
            var sql = "DELETE FROM department WHERE \"dept_id\"=@id RETURNING *";
            return await _connection.QueryFirstOrDefaultAsync<Department>(sql, new { id });
        }
        catch (Exception ex)
        {
            throw new RepositoryException($"Failed to remove department with ID {id}", ex);
        }
    }
}