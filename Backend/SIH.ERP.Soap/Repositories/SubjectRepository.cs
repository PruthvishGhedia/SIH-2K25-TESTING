using Dapper;
using SIH.ERP.Soap.Models;
using System.Data;
using SIH.ERP.Soap.Exceptions;

namespace SIH.ERP.Soap.Repositories;

public class SubjectRepository : RepositoryBase, ISubjectRepository
{
    public SubjectRepository(IDbConnection connection) : base(connection) { }

    public async Task<IEnumerable<Subject>> ListAsync(int limit, int offset)
    {
        try
        {
            EnsureConnection();
            return await _connection.QueryAsync<Subject>("SELECT * FROM subject ORDER BY subject_code LIMIT @limit OFFSET @offset", new { limit, offset });
        }
        catch (Exception ex)
        {
            throw new RepositoryException("Failed to list subjects", ex);
        }
    }

    public async Task<Subject?> GetAsync(int id)
    {
        try
        {
            EnsureConnection();
            return await _connection.QueryFirstOrDefaultAsync<Subject>("SELECT * FROM subject WHERE \"subject_code\"=@id", new { id });
        }
        catch (Exception ex)
        {
            throw new RepositoryException($"Failed to get subject with ID {id}", ex);
        }
    }

    public async Task<Subject> CreateAsync(Subject item)
    {
        try
        {
            EnsureConnection();
            return await _connection.QuerySingleAsync<Subject>("INSERT INTO subject(\"subject_code\",\"course_id\",\"subject_name\",\"credits\") VALUES (@subject_code,@course_id,@subject_name,@credits) RETURNING *", item);
        }
        catch (Exception ex)
        {
            throw new RepositoryException("Failed to create subject", ex);
        }
    }

    public async Task<Subject?> UpdateAsync(int id, Subject item)
    {
        try
        {
            EnsureConnection();
            return await _connection.QueryFirstOrDefaultAsync<Subject>("UPDATE subject SET \"course_id\"=@course_id, \"subject_name\"=@subject_name, \"credits\"=@credits WHERE \"subject_code\"=@id RETURNING *", new { id, item.course_id, item.subject_name, item.credits });
        }
        catch (Exception ex)
        {
            throw new RepositoryException($"Failed to update subject with ID {id}", ex);
        }
    }

    public async Task<Subject?> RemoveAsync(int id)
    {
        try
        {
            EnsureConnection();
            return await _connection.QueryFirstOrDefaultAsync<Subject>("DELETE FROM subject WHERE \"subject_code\"=@id RETURNING *", new { id });
        }
        catch (Exception ex)
        {
            throw new RepositoryException($"Failed to remove subject with ID {id}", ex);
        }
    }
}