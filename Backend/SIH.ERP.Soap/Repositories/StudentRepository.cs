using Dapper;
using SIH.ERP.Soap.Models;
using System.Data;

namespace SIH.ERP.Soap.Repositories;

public class StudentRepository
{
    private readonly IDbConnection _db;
    public StudentRepository(IDbConnection db) { _db = db; }

    public Task<IEnumerable<Student>> ListAsync(int limit, int offset) => _db.QueryAsync<Student>("SELECT * FROM student ORDER BY student_id LIMIT @limit OFFSET @offset", new { limit, offset });
    public Task<Student?> GetAsync(int id) => _db.QueryFirstOrDefaultAsync<Student>("SELECT * FROM student WHERE \"student_id\"=@id", new { id });
    public Task<Student> CreateAsync(Student item) => _db.QuerySingleAsync<Student>("INSERT INTO student(\"student_id\",\"user_id\",\"dept_id\",\"course_id\",\"admission_date\",\"verified\") VALUES (@student_id,@user_id,@dept_id,@course_id,@admission_date,@verified) RETURNING *", item);
    public Task<Student?> UpdateAsync(int id, Student item) => _db.QueryFirstOrDefaultAsync<Student>("UPDATE student SET \"user_id\"=@user_id, \"dept_id\"=@dept_id, \"course_id\"=@course_id, \"admission_date\"=@admission_date, \"verified\"=@verified WHERE \"student_id\"=@id RETURNING *", new { id, item.user_id, item.dept_id, item.course_id, item.admission_date, item.verified });
    public Task<Student?> RemoveAsync(int id) => _db.QueryFirstOrDefaultAsync<Student>("DELETE FROM student WHERE \"student_id\"=@id RETURNING *", new { id });
}

