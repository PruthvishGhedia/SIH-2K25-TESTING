using Dapper;
using SIH.ERP.Soap.Models;
using SIH.ERP.Soap.Exceptions;
using System.Data;

namespace SIH.ERP.Soap.Repositories;

public class AdmissionRepository : RepositoryBase, IAdmissionRepository
{
    public AdmissionRepository(IDbConnection connection) : base(connection) { }

    public async Task<IEnumerable<Admission>> ListAsync(int limit, int offset)
    {
        try
        {
            EnsureConnection();
            var sql = "SELECT * FROM admission ORDER BY admission_id LIMIT @limit OFFSET @offset";
            return await _connection.QueryAsync<Admission>(sql, new { limit, offset });
        }
        catch (Exception ex)
        {
            throw new RepositoryException("Failed to list admissions", ex);
        }
    }

    public async Task<Admission?> GetAsync(int id)
    {
        try
        {
            EnsureConnection();
            var sql = "SELECT * FROM admission WHERE \"admission_id\"=@id";
            return await _connection.QueryFirstOrDefaultAsync<Admission>(sql, new { id });
        }
        catch (Exception ex)
        {
            throw new RepositoryException($"Failed to get admission with ID {id}", ex);
        }
    }

    public async Task<Admission> CreateAsync(Admission item)
    {
        try
        {
            EnsureConnection();
            var sql = "INSERT INTO admission(\"admission_id\", \"full_name\", \"email\", \"dob\", \"contact_no\", \"address\", \"dept_id\", \"course_id\", \"applied_on\", \"verified\", \"confirmed\", \"created_at\", \"updated_at\") VALUES (@admission_id, @full_name, @email, @dob, @contact_no, @address, @dept_id, @course_id, @applied_on, @verified, @confirmed, @created_at, @updated_at) RETURNING *";
            return await _connection.QuerySingleAsync<Admission>(sql, item);
        }
        catch (Exception ex)
        {
            throw new RepositoryException("Failed to create admission", ex);
        }
    }

    public async Task<Admission?> UpdateAsync(int id, Admission item)
    {
        try
        {
            EnsureConnection();
            var sql = "UPDATE admission SET \"full_name\"=@full_name, \"email\"=@email, \"dob\"=@dob, \"contact_no\"=@contact_no, \"address\"=@address, \"dept_id\"=@dept_id, \"course_id\"=@course_id, \"applied_on\"=@applied_on, \"verified\"=@verified, \"confirmed\"=@confirmed, \"updated_at\"=@updated_at WHERE \"admission_id\"=@id RETURNING *";
            return await _connection.QueryFirstOrDefaultAsync<Admission>(sql, new { id, item.full_name, item.email, item.dob, item.contact_no, item.address, item.dept_id, item.course_id, item.applied_on, item.verified, item.confirmed, item.updated_at });
        }
        catch (Exception ex)
        {
            throw new RepositoryException($"Failed to update admission with ID {id}", ex);
        }
    }

    public async Task<Admission?> RemoveAsync(int id)
    {
        try
        {
            EnsureConnection();
            var sql = "DELETE FROM admission WHERE \"admission_id\"=@id RETURNING *";
            return await _connection.QueryFirstOrDefaultAsync<Admission>(sql, new { id });
        }
        catch (Exception ex)
        {
            throw new RepositoryException($"Failed to remove admission with ID {id}", ex);
        }
    }
}