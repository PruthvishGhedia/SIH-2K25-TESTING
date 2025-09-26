using Dapper;
using SIH.ERP.Soap.Models;
using SIH.ERP.Soap.Exceptions;
using System.Data;

namespace SIH.ERP.Soap.Repositories;

public class AttendanceRepository : RepositoryBase, IAttendanceRepository
{
    public AttendanceRepository(IDbConnection connection) : base(connection) { }

    public async Task<IEnumerable<Attendance>> ListAsync(int limit, int offset)
    {
        try
        {
            EnsureConnection();
            var sql = "SELECT * FROM attendance ORDER BY attendance_id LIMIT @limit OFFSET @offset";
            return await _connection.QueryAsync<Attendance>(sql, new { limit, offset });
        }
        catch (Exception ex)
        {
            throw new RepositoryException("Failed to list attendance", ex);
        }
    }

    public async Task<Attendance?> GetAsync(int id)
    {
        try
        {
            EnsureConnection();
            var sql = "SELECT * FROM attendance WHERE attendance_id = @id";
            return await _connection.QueryFirstOrDefaultAsync<Attendance>(sql, new { id });
        }
        catch (Exception ex)
        {
            throw new RepositoryException($"Failed to get attendance with ID {id}", ex);
        }
    }

    public async Task<Attendance> CreateAsync(Attendance item)
    {
        try
        {
            EnsureConnection();
            var sql = @"INSERT INTO attendance (student_id, course_id, date, present)
                        VALUES (@student_id, @course_id, @date, @present) RETURNING *";
            return await _connection.QuerySingleAsync<Attendance>(sql, item);
        }
        catch (Exception ex)
        {
            throw new RepositoryException("Failed to create attendance", ex);
        }
    }

    public async Task<Attendance?> UpdateAsync(int id, Attendance item)
    {
        try
        {
            EnsureConnection();
            var sql = @"UPDATE attendance SET student_id=@student_id, course_id=@course_id, date=@date, present=@present
                        WHERE attendance_id=@id RETURNING *";
            return await _connection.QueryFirstOrDefaultAsync<Attendance>(sql, new { id, item.student_id, item.course_id, item.date, item.present });
        }
        catch (Exception ex)
        {
            throw new RepositoryException($"Failed to update attendance with ID {id}", ex);
        }
    }

    public async Task<Attendance?> RemoveAsync(int id)
    {
        try
        {
            EnsureConnection();
            var sql = "DELETE FROM attendance WHERE attendance_id=@id RETURNING *";
            return await _connection.QueryFirstOrDefaultAsync<Attendance>(sql, new { id });
        }
        catch (Exception ex)
        {
            throw new RepositoryException($"Failed to remove attendance with ID {id}", ex);
        }
    }
}