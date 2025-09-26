using Dapper;
using SIH.ERP.Soap.Models;
using System.Data;

namespace SIH.ERP.Soap.Repositories;

public interface IExamRepository
{
    Task<IEnumerable<Exam>> ListAsync(int limit, int offset);
    Task<Exam?> GetAsync(int id);
    Task<Exam> CreateAsync(Exam item);
    Task<Exam?> UpdateAsync(int id, Exam item);
    Task<Exam?> RemoveAsync(int id);
}

public class ExamRepository : IExamRepository
{
    private readonly IDbConnection _db;
    public ExamRepository(IDbConnection db)
    {
        _db = db;
    }

    public async Task<IEnumerable<Exam>> ListAsync(int limit, int offset)
    {
        return await _db.QueryAsync<Exam>("SELECT * FROM exam ORDER BY exam_id LIMIT @limit OFFSET @offset", new { limit, offset });
    }

    public async Task<Exam?> GetAsync(int id)
    {
        return await _db.QueryFirstOrDefaultAsync<Exam>("SELECT * FROM exam WHERE \"exam_id\"=@id", new { id });
    }

    public async Task<Exam> CreateAsync(Exam item)
    {
        var sql = "INSERT INTO exam(\"exam_id\", \"dept_id\", \"subject_code\", \"exam_date\", \"assessment_type\", \"max_marks\", \"created_by\", \"created_at\", \"updated_at\") VALUES (@exam_id, @dept_id, @subject_code, @exam_date, @assessment_type, @max_marks, @created_by, @created_at, @updated_at) RETURNING *";
        return await _db.QuerySingleAsync<Exam>(sql, item);
    }

    public async Task<Exam?> UpdateAsync(int id, Exam item)
    {
        var sql = "UPDATE exam SET \"dept_id\"=@dept_id, \"subject_code\"=@subject_code, \"exam_date\"=@exam_date, \"assessment_type\"=@assessment_type, \"max_marks\"=@max_marks, \"created_by\"=@created_by, \"updated_at\"=@updated_at WHERE \"exam_id\"=@id RETURNING *";
        return await _db.QueryFirstOrDefaultAsync<Exam>(sql, new { id, item.dept_id, item.subject_code, item.exam_date, item.assessment_type, item.max_marks, item.created_by, item.updated_at });
    }

    public async Task<Exam?> RemoveAsync(int id)
    {
        var sql = "DELETE FROM exam WHERE \"exam_id\"=@id RETURNING *";
        return await _db.QueryFirstOrDefaultAsync<Exam>(sql, new { id });
    }
}