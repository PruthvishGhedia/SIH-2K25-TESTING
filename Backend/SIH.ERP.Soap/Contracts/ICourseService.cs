using CoreWCF;
using SIH.ERP.Soap.Models;

namespace SIH.ERP.Soap.Contracts;

/// <summary>
/// Service contract for managing courses in the ERP system
/// </summary>
[ServiceContract]
public interface ICourseService
{
    /// <summary>
    /// Retrieves a list of courses with pagination
    /// </summary>
    /// <param name="limit">Maximum number of courses to retrieve (default: 100)</param>
    /// <param name="offset">Number of courses to skip (default: 0)</param>
    /// <returns>A collection of Course objects</returns>
    [OperationContract]
    Task<IEnumerable<Course>> ListAsync(int limit = 100, int offset = 0);

    /// <summary>
    /// Retrieves a specific course by ID
    /// </summary>
    /// <param name="course_id">The unique identifier of the course</param>
    /// <returns>The Course object if found, null otherwise</returns>
    [OperationContract]
    Task<Course?> GetAsync(int course_id);

    /// <summary>
    /// Creates a new course record
    /// </summary>
    /// <param name="item">The course object to create</param>
    /// <returns>The created Course object with assigned ID</returns>
    [OperationContract]
    Task<Course> CreateAsync(Course item);

    /// <summary>
    /// Updates an existing course record
    /// </summary>
    /// <param name="course_id">The unique identifier of the course to update</param>
    /// <param name="item">The course object with updated information</param>
    /// <returns>The updated Course object if successful, null otherwise</returns>
    [OperationContract]
    Task<Course?> UpdateAsync(int course_id, Course item);

    /// <summary>
    /// Removes a course record by ID
    /// </summary>
    /// <param name="course_id">The unique identifier of the course to remove</param>
    /// <returns>The removed Course object if successful, null otherwise</returns>
    [OperationContract]
    Task<Course?> RemoveAsync(int course_id);
}