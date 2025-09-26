using Dapper;
using SIH.ERP.Soap.Models;
using System.Data;
using SIH.ERP.Soap.Exceptions;

namespace SIH.ERP.Soap.Repositories;

public class ExamRepository : RepositoryBase, IExamRepository
{
    public ExamRepository(IDbConnection connection) : base(connection) { }

    public async Task<IEnumerable<Exam>> ListAsync(int limit, int offset)
    {
        try
        {
            EnsureConnection();
            return await _connection.QueryAsync<Exam>("SELECT * FROM exam ORDER BY exam_id LIMIT @limit OFFSET @offset", new { limit, offset });
        }
        catch (Exception ex)
        {
            throw new RepositoryException("Failed to list exams", ex);
        }
    }

    public async Task<Exam?> GetAsync(int id)
    {
        try
        {
            EnsureConnection();
            return await _connection.QueryFirstOrDefaultAsync<Exam>("SELECT * FROM exam WHERE \"exam_id\"=@id", new { id });
        }
        catch (Exception ex)
        {
            throw new RepositoryException($"Failed to get exam with ID {id}", ex);
        }
    }

    public async Task<Exam> CreateAsync(Exam item)
    {
        try
        {
            EnsureConnection();
            var sql = "INSERT INTO exam(\"exam_id\", \"dept_id\", \"subject_code\", \"exam_date\", \"assessment_type\", \"max_marks\", \"created_by\", \"created_at\", \"updated_at\") VALUES (@exam_id, @dept_id, @subject_code, @exam_date, @assessment_type, @max_marks, @created_by, @created_at, @updated_at) RETURNING *";
            return await _connection.QuerySingleAsync<Exam>(sql, item);
        }
        catch (Exception ex)
        {
            throw new RepositoryException("Failed to create exam", ex);
        }
    }

    public async Task<Exam?> UpdateAsync(int id, Exam item)
    {
        try
        {
            EnsureConnection();
            var sql = "UPDATE exam SET \"dept_id\"=@dept_id, \"subject_code\"=@subject_code, \"exam_date\"=@exam_date, \"assessment_type\"=@assessment_type, \"max_marks\"=@max_marks, \"created_by\"=@created_by, \"updated_at\"=@updated_at WHERE \"exam_id\"=@id RETURNING *";
            return await _connection.QueryFirstOrDefaultAsync<Exam>(sql, new { id, item.dept_id, item.subject_code, item.exam_date, item.assessment_type, item.max_marks, item.created_by, item.updated_at });
        }
        catch (Exception ex)
        {
            throw new RepositoryException($"Failed to update exam with ID {id}", ex);
        }
    }

    public async Task<Exam?> RemoveAsync(int id)
    {
        try
        {
            EnsureConnection();
            var sql = "DELETE FROM exam WHERE \"exam_id\"=@id RETURNING *";
            return await _connection.QueryFirstOrDefaultAsync<Exam>(sql, new { id });
        }
        catch (Exception ex)
        {
            throw new RepositoryException($"Failed to remove exam with ID {id}", ex);
        }
    }
}