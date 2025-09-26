using CoreWCF;
using CoreWCF.Web;
using SIH.ERP.Soap.Models;

namespace SIH.ERP.Soap.Contracts;

/// <summary>
/// Service contract for managing students in the ERP system.
/// This service handles student records, including personal information, academic details, and enrollment data.
/// </summary>
[ServiceContract]
public interface IStudentService
{
    /// <summary>
    /// Retrieves a list of students with pagination support.
    /// Use this endpoint to get multiple student records with optional pagination.
    /// </summary>
    /// <param name="limit">Maximum number of students to retrieve (default: 100, maximum: 1000)</param>
    /// <param name="offset">Number of students to skip for pagination (default: 0)</param>
    /// <returns>A collection of Student objects</returns>
    [OperationContract]
    [WebGet(UriTemplate = "/students?limit={limit}&offset={offset}", ResponseFormat = WebMessageFormat.Json)]
    Task<IEnumerable<Student>> ListAsync(int limit = 100, int offset = 0);
    
    /// <summary>
    /// Retrieves a specific student by its unique identifier.
    /// Use this endpoint to get detailed information about a specific student.
    /// </summary>
    /// <param name="student_id">The unique identifier of the student record (must be greater than 0)</param>
    /// <returns>The Student object if found, null otherwise</returns>
    [OperationContract]
    [WebGet(UriTemplate = "/students/{student_id}", ResponseFormat = WebMessageFormat.Json)]
    Task<Student?> GetAsync(string student_id);
    
    /// <summary>
    /// Creates a new student record in the system.
    /// Use this endpoint to add a new student to the institution.
    /// </summary>
    /// <param name="item">The student object to create. The student_id field is ignored and will be assigned automatically.</param>
    /// <returns>The created Student object with assigned ID</returns>
    [OperationContract]
    [WebInvoke(Method = "POST", UriTemplate = "/students", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
    Task<Student> CreateAsync(Student item);
    
    /// <summary>
    /// Updates an existing student record with new information.
    /// Use this endpoint to modify an existing student record, such as updating contact information or academic details.
    /// </summary>
    /// <param name="student_id">The unique identifier of the student to update (must match the ID in the item parameter)</param>
    /// <param name="item">The student object with updated information</param>
    /// <returns>The updated Student object if successful, null otherwise</returns>
    [OperationContract]
    [WebInvoke(Method = "PUT", UriTemplate = "/students/{student_id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
    Task<Student?> UpdateAsync(string student_id, Student item);
    
    /// <summary>
    /// Removes a student record from the system by its unique identifier.
    /// Use this endpoint to delete a student record, typically used for data correction or privacy compliance.
    /// </summary>
    /// <param name="student_id">The unique identifier of the student to remove (must be greater than 0)</param>
    /// <returns>The removed Student object if successful, null otherwise</returns>
    [OperationContract]
    [WebInvoke(Method = "DELETE", UriTemplate = "/students/{student_id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
    Task<Student?> RemoveAsync(string student_id);
}