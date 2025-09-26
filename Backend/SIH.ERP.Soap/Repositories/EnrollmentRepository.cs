using Dapper;
using SIH.ERP.Soap.Models;
using SIH.ERP.Soap.Exceptions;
using System.Data;

namespace SIH.ERP.Soap.Repositories;

public class EnrollmentRepository : RepositoryBase, IEnrollmentRepository
{
    public EnrollmentRepository(IDbConnection connection) : base(connection) { }

    public async Task<IEnumerable<Enrollment>> ListAsync(int limit, int offset)
    {
        try
        {
            EnsureConnection();
            var sql = "SELECT * FROM enrollment ORDER BY enrollment_id LIMIT @limit OFFSET @offset";
            return await _connection.QueryAsync<Enrollment>(sql, new { limit, offset });
        }
        catch (Exception ex)
        {
            throw new RepositoryException("Failed to list enrollments", ex);
        }
    }

    public async Task<Enrollment?> GetAsync(int id)
    {
        try
        {
            EnsureConnection();
            var sql = "SELECT * FROM enrollment WHERE enrollment_id = @id";
            return await _connection.QueryFirstOrDefaultAsync<Enrollment>(sql, new { id });
        }
        catch (Exception ex)
        {
            throw new RepositoryException($"Failed to get enrollment with ID {id}", ex);
        }
    }

    public async Task<Enrollment> CreateAsync(Enrollment item)
    {
        try
        {
            EnsureConnection();
            var sql = @"INSERT INTO enrollment (student_id, course_id, enrollment_date, status)
                        VALUES (@student_id, @course_id, @enrollment_date, @status) RETURNING *";
            return await _connection.QuerySingleAsync<Enrollment>(sql, item);
        }
        catch (Exception ex)
        {
            throw new RepositoryException("Failed to create enrollment", ex);
        }
    }

    public async Task<Enrollment?> UpdateAsync(int id, Enrollment item)
    {
        try
        {
            EnsureConnection();
            var sql = @"UPDATE enrollment SET student_id=@student_id, course_id=@course_id, enrollment_date=@enrollment_date, status=@status
                        WHERE enrollment_id=@id RETURNING *";
            return await _connection.QueryFirstOrDefaultAsync<Enrollment>(sql, new { id, item.student_id, item.course_id, item.enrollment_date, item.status });
        }
        catch (Exception ex)
        {
            throw new RepositoryException($"Failed to update enrollment with ID {id}", ex);
        }
    }

    public async Task<Enrollment?> RemoveAsync(int id)
    {
        try
        {
            EnsureConnection();
            var sql = "DELETE FROM enrollment WHERE enrollment_id=@id RETURNING *";
            return await _connection.QueryFirstOrDefaultAsync<Enrollment>(sql, new { id });
        }
        catch (Exception ex)
        {
            throw new RepositoryException($"Failed to remove enrollment with ID {id}", ex);
        }
    }
}