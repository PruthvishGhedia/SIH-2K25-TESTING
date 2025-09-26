using Dapper;
using SIH.ERP.Soap.Models;
using System.Data;

namespace SIH.ERP.Soap.Repositories;

public class AdmissionRepository
{
    private readonly IDbConnection _db;
    public AdmissionRepository(IDbConnection db)
    {
        _db = db;
    }

    public async Task<IEnumerable<Admission>> ListAsync(int limit, int offset)
    {
        return await _db.QueryAsync<Admission>("SELECT * FROM admission ORDER BY admission_id LIMIT @limit OFFSET @offset", new { limit, offset });
    }

    public async Task<Admission?> GetAsync(int id)
    {
        return await _db.QueryFirstOrDefaultAsync<Admission>("SELECT * FROM admission WHERE \"admission_id\"=@id", new { id });
    }

    public async Task<Admission> CreateAsync(Admission item)
    {
        var sql = "INSERT INTO admission(\"admission_id\", \"full_name\", \"email\", \"dob\", \"contact_no\", \"address\", \"dept_id\", \"course_id\", \"applied_on\", \"verified\", \"confirmed\", \"created_at\", \"updated_at\") VALUES (@admission_id, @full_name, @email, @dob, @contact_no, @address, @dept_id, @course_id, @applied_on, @verified, @confirmed, @created_at, @updated_at) RETURNING *";
        return await _db.QuerySingleAsync<Admission>(sql, item);
    }

    public async Task<Admission?> UpdateAsync(int id, Admission item)
    {
        var sql = "UPDATE admission SET \"full_name\"=@full_name, \"email\"=@email, \"dob\"=@dob, \"contact_no\"=@contact_no, \"address\"=@address, \"dept_id\"=@dept_id, \"course_id\"=@course_id, \"applied_on\"=@applied_on, \"verified\"=@verified, \"confirmed\"=@confirmed, \"updated_at\"=@updated_at WHERE \"admission_id\"=@id RETURNING *";
        return await _db.QueryFirstOrDefaultAsync<Admission>(sql, new { id, item.full_name, item.email, item.dob, item.contact_no, item.address, item.dept_id, item.course_id, item.applied_on, item.verified, item.confirmed, item.updated_at });
    }

    public async Task<Admission?> RemoveAsync(int id)
    {
        var sql = "DELETE FROM admission WHERE \"admission_id\"=@id RETURNING *";
        return await _db.QueryFirstOrDefaultAsync<Admission>(sql, new { id });
    }
}