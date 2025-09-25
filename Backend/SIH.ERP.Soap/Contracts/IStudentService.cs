using CoreWCF;
using SIH.ERP.Soap.Models;

namespace SIH.ERP.Soap.Contracts;

/// <summary>
/// Service contract for managing students in the ERP system
/// </summary>
[ServiceContract]
public interface IStudentService
{
    /// <summary>
    /// Retrieves a list of students with pagination
    /// </summary>
    /// <param name="limit">Maximum number of students to retrieve (default: 100)</param>
    /// <param name="offset">Number of students to skip (default: 0)</param>
    /// <returns>A collection of Student objects</returns>
    [OperationContract]
    Task<IEnumerable<Student>> ListAsync(int limit = 100, int offset = 0);
    
    /// <summary>
    /// Retrieves a specific student by ID
    /// </summary>
    /// <param name="student_id">The unique identifier of the student</param>
    /// <returns>The Student object if found, null otherwise</returns>
    [OperationContract]
    Task<Student?> GetAsync(int student_id);
    
    /// <summary>
    /// Creates a new student record
    /// </summary>
    /// <param name="item">The student object to create</param>
    /// <returns>The created Student object with assigned ID</returns>
    [OperationContract]
    Task<Student> CreateAsync(Student item);
    
    /// <summary>
    /// Updates an existing student record
    /// </summary>
    /// <param name="student_id">The unique identifier of the student to update</param>
    /// <param name="item">The student object with updated information</param>
    /// <returns>The updated Student object if successful, null otherwise</returns>
    [OperationContract]
    Task<Student?> UpdateAsync(int student_id, Student item);
    
    /// <summary>
    /// Removes a student record by ID
    /// </summary>
    /// <param name="student_id">The unique identifier of the student to remove</param>
    /// <returns>The removed Student object if successful, null otherwise</returns>
    [OperationContract]
    Task<Student?> RemoveAsync(int student_id);
}