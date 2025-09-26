using CoreWCF;
using CoreWCF.Web;
using SIH.ERP.Soap.Models;

namespace SIH.ERP.Soap.Contracts;

/// <summary>
/// Service contract for managing student enrollments in the ERP system.
/// This service handles student course enrollments, including enrollment dates and status tracking.
/// </summary>
[ServiceContract]
public interface IEnrollmentService
{
    /// <summary>
    /// Retrieves a list of enrollments with pagination support.
    /// Use this endpoint to get multiple enrollment records with optional pagination.
    /// </summary>
    /// <param name="limit">Maximum number of enrollments to retrieve (default: 100, maximum: 1000)</param>
    /// <param name="offset">Number of enrollments to skip for pagination (default: 0)</param>
    /// <returns>A collection of Enrollment objects</returns>
    /// <example>
    /// <code>
    /// // Get first 10 enrollments
    /// var enrollments = await ListAsync(10, 0);
    /// </code>
    /// </example>
    [OperationContract]
    [WebGet(UriTemplate = "/enrollments?limit={limit}&offset={offset}", ResponseFormat = WebMessageFormat.Json)]
    Task<IEnumerable<Enrollment>> ListAsync(int limit = 100, int offset = 0);

    /// <summary>
    /// Retrieves a specific enrollment record by its unique identifier.
    /// Use this endpoint to get detailed information about a specific student's course enrollment.
    /// </summary>
    /// <param name="enrollment_id">The unique identifier of the enrollment record (must be greater than 0)</param>
    /// <returns>The Enrollment object if found, null otherwise</returns>
    /// <example>
    /// <code>
    /// // Get enrollment with ID 1
    /// var enrollment = await GetAsync("1");
    /// </code>
    /// </example>
    [OperationContract]
    [WebGet(UriTemplate = "/enrollments/{enrollment_id}", ResponseFormat = WebMessageFormat.Json)]
    Task<Enrollment?> GetAsync(string enrollment_id);

    /// <summary>
    /// Creates a new enrollment record in the system.
    /// Use this endpoint to enroll a student in a course.
    /// </summary>
    /// <param name="item">The enrollment object to create. The enrollment_id field is ignored and will be assigned automatically.</param>
    /// <returns>The created Enrollment object with assigned ID</returns>
    /// <example>
    /// <code>
    /// // Create a new enrollment
    /// var newEnrollment = new Enrollment 
    /// {
    ///     student_id = 1,
    ///     course_id = 101,
    ///     enrollment_date = "2025-09-01",
    ///     status = "Active"
    /// };
    /// var createdEnrollment = await CreateAsync(newEnrollment);
    /// </code>
    /// </example>
    [OperationContract]
    [WebInvoke(Method = "POST", UriTemplate = "/enrollments", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
    Task<Enrollment> CreateAsync(Enrollment item);

    /// <summary>
    /// Updates an existing enrollment record with new information.
    /// Use this endpoint to modify an existing enrollment, such as updating status or enrollment date.
    /// </summary>
    /// <param name="enrollment_id">The unique identifier of the enrollment to update (must match the ID in the item parameter)</param>
    /// <param name="item">The enrollment object with updated information</param>
    /// <returns>The updated Enrollment object if successful, null otherwise</returns>
    /// <example>
    /// <code>
    /// // Update enrollment with ID 1
    /// var updatedEnrollment = new Enrollment 
    /// {
    ///     enrollment_id = 1,
    ///     student_id = 1,
    ///     course_id = 101,
    ///     enrollment_date = "2025-09-01",
    ///     status = "Completed"
    /// };
    /// var result = await UpdateAsync("1", updatedEnrollment);
    /// </code>
    /// </example>
    [OperationContract]
    [WebInvoke(Method = "PUT", UriTemplate = "/enrollments/{enrollment_id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
    Task<Enrollment?> UpdateAsync(string enrollment_id, Enrollment item);

    /// <summary>
    /// Removes an enrollment record from the system by its unique identifier.
    /// Use this endpoint to delete an enrollment record, typically used for data correction.
    /// </summary>
    /// <param name="enrollment_id">The unique identifier of the enrollment to remove (must be greater than 0)</param>
    /// <returns>The removed Enrollment object if successful, null otherwise</returns>
    /// <example>
    /// <code>
    /// // Remove enrollment with ID 1
    /// var removedEnrollment = await RemoveAsync("1");
    /// </code>
    /// </example>
    [OperationContract]
    [WebInvoke(Method = "DELETE", UriTemplate = "/enrollments/{enrollment_id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
    Task<Enrollment?> RemoveAsync(string enrollment_id);
}