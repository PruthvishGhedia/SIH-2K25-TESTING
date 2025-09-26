using Dapper;
using SIH.ERP.Soap.Models;
using System.Data;

namespace SIH.ERP.Soap.Repositories;

public class SubjectRepository
{
    private readonly IDbConnection _db;
    public SubjectRepository(IDbConnection db) { _db = db; }

    public Task<IEnumerable<Subject>> ListAsync(int limit, int offset) => _db.QueryAsync<Subject>("SELECT * FROM subject ORDER BY subject_code LIMIT @limit OFFSET @offset", new { limit, offset });
    public Task<Subject?> GetAsync(int id) => _db.QueryFirstOrDefaultAsync<Subject>("SELECT * FROM subject WHERE \"subject_code\"=@id", new { id });
    public Task<Subject> CreateAsync(Subject item) => _db.QuerySingleAsync<Subject>("INSERT INTO subject(\"subject_code\",\"course_id\",\"subject_name\",\"credits\") VALUES (@subject_code,@course_id,@subject_name,@credits) RETURNING *", item);
    public Task<Subject?> UpdateAsync(int id, Subject item) => _db.QueryFirstOrDefaultAsync<Subject>("UPDATE subject SET \"course_id\"=@course_id, \"subject_name\"=@subject_name, \"credits\"=@credits WHERE \"subject_code\"=@id RETURNING *", new { id, item.course_id, item.subject_name, item.credits });
    public Task<Subject?> RemoveAsync(int id) => _db.QueryFirstOrDefaultAsync<Subject>("DELETE FROM subject WHERE \"subject_code\"=@id RETURNING *", new { id });
}

