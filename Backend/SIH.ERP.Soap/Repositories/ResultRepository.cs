using Dapper;
using SIH.ERP.Soap.Models;
using System.Data;
using SIH.ERP.Soap.Exceptions;

namespace SIH.ERP.Soap.Repositories;

public class ResultRepository : RepositoryBase, IResultRepository
{
    public ResultRepository(IDbConnection connection) : base(connection) { }

    public async Task<IEnumerable<Result>> ListAsync(int limit, int offset)
    {
        try
        {
            EnsureConnection();
            return await _connection.QueryAsync<Result>("SELECT * FROM result ORDER BY result_id LIMIT @limit OFFSET @offset", new { limit, offset });
        }
        catch (Exception ex)
        {
            throw new RepositoryException("Failed to list results", ex);
        }
    }

    public async Task<Result?> GetAsync(int id)
    {
        try
        {
            EnsureConnection();
            return await _connection.QueryFirstOrDefaultAsync<Result>("SELECT * FROM result WHERE \"result_id\"=@id", new { id });
        }
        catch (Exception ex)
        {
            throw new RepositoryException($"Failed to get result with ID {id}", ex);
        }
    }

    public async Task<Result> CreateAsync(Result item)
    {
        try
        {
            EnsureConnection();
            var sql = "INSERT INTO result(\"result_id\", \"exam_id\", \"student_id\", \"marks\", \"grade\") VALUES (@result_id, @exam_id, @student_id, @marks, @grade) RETURNING *";
            return await _connection.QuerySingleAsync<Result>(sql, item);
        }
        catch (Exception ex)
        {
            throw new RepositoryException("Failed to create result", ex);
        }
    }

    public async Task<Result?> UpdateAsync(int id, Result item)
    {
        try
        {
            EnsureConnection();
            var sql = "UPDATE result SET \"exam_id\"=@exam_id, \"student_id\"=@student_id, \"marks\"=@marks, \"grade\"=@grade WHERE \"result_id\"=@id RETURNING *";
            return await _connection.QueryFirstOrDefaultAsync<Result>(sql, new { id, item.exam_id, item.student_id, item.marks, item.grade });
        }
        catch (Exception ex)
        {
            throw new RepositoryException($"Failed to update result with ID {id}", ex);
        }
    }

    public async Task<Result?> RemoveAsync(int id)
    {
        try
        {
            EnsureConnection();
            var sql = "DELETE FROM result WHERE \"result_id\"=@id RETURNING *";
            return await _connection.QueryFirstOrDefaultAsync<Result>(sql, new { id });
        }
        catch (Exception ex)
        {
            throw new RepositoryException($"Failed to remove result with ID {id}", ex);
        }
    }
}