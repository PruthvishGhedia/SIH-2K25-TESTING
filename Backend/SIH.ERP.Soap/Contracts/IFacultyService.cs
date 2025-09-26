using CoreWCF;
using CoreWCF.Web;
using SIH.ERP.Soap.Models;

namespace SIH.ERP.Soap.Contracts;

/// <summary>
/// Service contract for managing faculty members in the ERP system.
/// This service handles faculty information, including personal details, department association, and employment status.
/// </summary>
[ServiceContract]
public interface IFacultyService
{
    /// <summary>
    /// Retrieves a list of faculty members with pagination support.
    /// Use this endpoint to get multiple faculty records with optional pagination.
    /// </summary>
    /// <param name="limit">Maximum number of faculty members to retrieve (default: 100, maximum: 1000)</param>
    /// <param name="offset">Number of faculty members to skip for pagination (default: 0)</param>
    /// <returns>A collection of Faculty objects</returns>
    /// <example>
    /// <code>
    /// // Get first 10 faculty members
    /// var faculty = await ListAsync(10, 0);
    /// </code>
    /// </example>
    [OperationContract]
    [WebGet(UriTemplate = "/faculty?limit={limit}&offset={offset}", ResponseFormat = WebMessageFormat.Json)]
    Task<IEnumerable<Faculty>> ListAsync(int limit = 100, int offset = 0);

    /// <summary>
    /// Retrieves a specific faculty member record by its unique identifier.
    /// Use this endpoint to get detailed information about a specific faculty member.
    /// </summary>
    /// <param name="faculty_id">The unique identifier of the faculty member record (must be greater than 0)</param>
    /// <returns>The Faculty object if found, null otherwise</returns>
    /// <example>
    /// <code>
    /// // Get faculty member with ID 1
    /// var faculty = await GetAsync("1");
    /// </code>
    /// </example>
    [OperationContract]
    [WebGet(UriTemplate = "/faculty/{faculty_id}", ResponseFormat = WebMessageFormat.Json)]
    Task<Faculty?> GetAsync(string faculty_id);

    /// <summary>
    /// Creates a new faculty member record in the system.
    /// Use this endpoint to add a new faculty member to the institution.
    /// </summary>
    /// <param name="item">The faculty object to create. The faculty_id field is ignored and will be assigned automatically.</param>
    /// <returns>The created Faculty object with assigned ID</returns>
    /// <example>
    /// <code>
    /// // Create a new faculty member
    /// var newFaculty = new Faculty 
    /// {
    ///     first_name = "John",
    ///     last_name = "Smith",
    ///     email = "john.smith@institution.edu",
    ///     phone = "+1234567890",
    ///     department_id = 1,
    ///     is_active = true
    /// };
    /// var createdFaculty = await CreateAsync(newFaculty);
    /// </code>
    /// </example>
    [OperationContract]
    [WebInvoke(Method = "POST", UriTemplate = "/faculty", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
    Task<Faculty> CreateAsync(Faculty item);

    /// <summary>
    /// Updates an existing faculty member record with new information.
    /// Use this endpoint to modify an existing faculty record, such as updating contact information or department.
    /// </summary>
    /// <param name="faculty_id">The unique identifier of the faculty member to update (must match the ID in the item parameter)</param>
    /// <param name="item">The faculty object with updated information</param>
    /// <returns>The updated Faculty object if successful, null otherwise</returns>
    /// <example>
    /// <code>
    /// // Update faculty member with ID 1
    /// var updatedFaculty = new Faculty 
    /// {
    ///     faculty_id = 1,
    ///     first_name = "John",
    ///     last_name = "Smith",
    ///     email = "john.smith@institution.edu",
    ///     phone = "+1234567899",
    ///     department_id = 2,
    ///     is_active = true
    /// };
    /// var result = await UpdateAsync("1", updatedFaculty);
    /// </code>
    /// </example>
    [OperationContract]
    [WebInvoke(Method = "PUT", UriTemplate = "/faculty/{faculty_id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
    Task<Faculty?> UpdateAsync(string faculty_id, Faculty item);

    /// <summary>
    /// Removes a faculty member record from the system by its unique identifier.
    /// Use this endpoint to delete a faculty record, typically used when faculty members leave the institution.
    /// </summary>
    /// <param name="faculty_id">The unique identifier of the faculty member to remove (must be greater than 0)</param>
    /// <returns>The removed Faculty object if successful, null otherwise</returns>
    /// <example>
    /// <code>
    /// // Remove faculty member with ID 1
    /// var removedFaculty = await RemoveAsync("1");
    /// </code>
    /// </example>
    [OperationContract]
    [WebInvoke(Method = "DELETE", UriTemplate = "/faculty/{faculty_id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
    Task<Faculty?> RemoveAsync(string faculty_id);
}