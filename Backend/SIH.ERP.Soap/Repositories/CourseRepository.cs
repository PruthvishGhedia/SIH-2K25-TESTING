using Dapper;
using SIH.ERP.Soap.Models;
using System.Data;
using SIH.ERP.Soap.Exceptions;

namespace SIH.ERP.Soap.Repositories;

public class CourseRepository : RepositoryBase, ICourseRepository
{
    public CourseRepository(IDbConnection connection) : base(connection) { }

    public async Task<IEnumerable<Course>> ListAsync(int limit, int offset)
    {
        try
        {
            EnsureConnection();
            return await _connection.QueryAsync<Course>("SELECT * FROM course ORDER BY course_id LIMIT @limit OFFSET @offset", new { limit, offset });
        }
        catch (Exception ex)
        {
            throw new RepositoryException("Failed to list courses", ex);
        }
    }

    public async Task<Course?> GetAsync(int id)
    {
        try
        {
            EnsureConnection();
            return await _connection.QueryFirstOrDefaultAsync<Course>("SELECT * FROM course WHERE \"course_id\"=@id", new { id });
        }
        catch (Exception ex)
        {
            throw new RepositoryException($"Failed to get course with ID {id}", ex);
        }
    }

    public async Task<Course> CreateAsync(Course item)
    {
        try
        {
            EnsureConnection();
            return await _connection.QuerySingleAsync<Course>("INSERT INTO course(\"course_id\",\"dept_id\",\"course_name\",\"course_code\") VALUES (@course_id,@dept_id,@course_name,@course_code) RETURNING *", item);
        }
        catch (Exception ex)
        {
            throw new RepositoryException("Failed to create course", ex);
        }
    }

    public async Task<Course?> UpdateAsync(int id, Course item)
    {
        try
        {
            EnsureConnection();
            return await _connection.QueryFirstOrDefaultAsync<Course>("UPDATE course SET \"dept_id\"=@dept_id, \"course_name\"=@course_name, \"course_code\"=@course_code WHERE \"course_id\"=@id RETURNING *", new { id, item.dept_id, item.course_name, item.course_code });
        }
        catch (Exception ex)
        {
            throw new RepositoryException($"Failed to update course with ID {id}", ex);
        }
    }

    public async Task<Course?> RemoveAsync(int id)
    {
        try
        {
            EnsureConnection();
            return await _connection.QueryFirstOrDefaultAsync<Course>("DELETE FROM course WHERE \"course_id\"=@id RETURNING *", new { id });
        }
        catch (Exception ex)
        {
            throw new RepositoryException($"Failed to remove course with ID {id}", ex);
        }
    }
}