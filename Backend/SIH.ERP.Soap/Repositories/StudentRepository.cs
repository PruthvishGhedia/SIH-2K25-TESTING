using Dapper;
using SIH.ERP.Soap.Models;
using SIH.ERP.Soap.Exceptions;
using System.Data;

namespace SIH.ERP.Soap.Repositories;

public interface IStudentRepository
{
    Task<IEnumerable<Student>> ListAsync(int limit, int offset);
    Task<Student?> GetAsync(int id);
    Task<Student> CreateAsync(Student item);
    Task<Student?> UpdateAsync(int id, Student item);
    Task<Student?> RemoveAsync(int id);
}

public class StudentRepository : RepositoryBase, IStudentRepository
{
    public StudentRepository(IDbConnection connection) : base(connection) { }

    public async Task<IEnumerable<Student>> ListAsync(int limit, int offset)
    {
        try
        {
            EnsureConnection();
            var sql = "SELECT * FROM student ORDER BY student_id LIMIT @limit OFFSET @offset";
            return await _connection.QueryAsync<Student>(sql, new { limit, offset });
        }
        catch (Exception ex)
        {
            throw new RepositoryException("Failed to list students", ex);
        }
    }

    public async Task<Student?> GetAsync(int id)
    {
        try
        {
            EnsureConnection();
            var sql = "SELECT * FROM student WHERE student_id = @id";
            return await _connection.QueryFirstOrDefaultAsync<Student>(sql, new { id });
        }
        catch (Exception ex)
        {
            throw new RepositoryException($"Failed to get student with ID {id}", ex);
        }
    }

    public async Task<Student> CreateAsync(Student item)
    {
        try
        {
            EnsureConnection();
            var sql = @"INSERT INTO student (first_name, last_name, dob, email, department_id, course_id, guardian_id, admission_date, verified) 
                        VALUES (@first_name, @last_name, @dob, @email, @department_id, @course_id, @guardian_id, @admission_date, @verified) 
                        RETURNING *";
            return await _connection.QuerySingleAsync<Student>(sql, item);
        }
        catch (Exception ex)
        {
            throw new RepositoryException("Failed to create student", ex);
        }
    }

    public async Task<Student?> UpdateAsync(int id, Student item)
    {
        try
        {
            EnsureConnection();
            var sql = @"UPDATE student SET first_name = @first_name, last_name = @last_name, dob = @dob, email = @email, 
                        department_id = @department_id, course_id = @course_id, guardian_id = @guardian_id, 
                        admission_date = @admission_date, verified = @verified 
                        WHERE student_id = @id RETURNING *";
            return await _connection.QueryFirstOrDefaultAsync<Student>(sql, new { 
                id, 
                item.first_name, 
                item.last_name, 
                item.dob, 
                item.email, 
                item.department_id, 
                item.course_id, 
                item.guardian_id, 
                item.admission_date, 
                item.verified 
            });
        }
        catch (Exception ex)
        {
            throw new RepositoryException($"Failed to update student with ID {id}", ex);
        }
    }

    public async Task<Student?> RemoveAsync(int id)
    {
        try
        {
            EnsureConnection();
            var sql = "DELETE FROM student WHERE student_id = @id RETURNING *";
            return await _connection.QueryFirstOrDefaultAsync<Student>(sql, new { id });
        }
        catch (Exception ex)
        {
            throw new RepositoryException($"Failed to remove student with ID {id}", ex);
        }
    }
}

