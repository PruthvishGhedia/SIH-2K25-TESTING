using Dapper;
using SIH.ERP.Soap.Models;
using System.Data;

namespace SIH.ERP.Soap.Repositories;

public interface ICourseRepository
{
    Task<IEnumerable<Course>> ListAsync(int limit, int offset);
    Task<Course?> GetAsync(int id);
    Task<Course> CreateAsync(Course item);
    Task<Course?> UpdateAsync(int id, Course item);
    Task<Course?> RemoveAsync(int id);
}

public class CourseRepository : ICourseRepository
{
    private readonly IDbConnection _db;
    public CourseRepository(IDbConnection db) { _db = db; }

    public Task<IEnumerable<Course>> ListAsync(int limit, int offset) => _db.QueryAsync<Course>("SELECT * FROM course ORDER BY course_id LIMIT @limit OFFSET @offset", new { limit, offset });
    public Task<Course?> GetAsync(int id) => _db.QueryFirstOrDefaultAsync<Course>("SELECT * FROM course WHERE \"course_id\"=@id", new { id });
    public Task<Course> CreateAsync(Course item) => _db.QuerySingleAsync<Course>("INSERT INTO course(\"course_id\",\"dept_id\",\"course_name\",\"course_code\") VALUES (@course_id,@dept_id,@course_name,@course_code) RETURNING *", item);
    public Task<Course?> UpdateAsync(int id, Course item) => _db.QueryFirstOrDefaultAsync<Course>("UPDATE course SET \"dept_id\"=@dept_id, \"course_name\"=@course_name, \"course_code\"=@course_code WHERE \"course_id\"=@id RETURNING *", new { id, item.dept_id, item.course_name, item.course_code });
    public Task<Course?> RemoveAsync(int id) => _db.QueryFirstOrDefaultAsync<Course>("DELETE FROM course WHERE \"course_id\"=@id RETURNING *", new { id });
}