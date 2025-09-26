using CoreWCF;
using CoreWCF.Web;
using SIH.ERP.Soap.Models;

namespace SIH.ERP.Soap.Contracts;

/// <summary>
/// Service contract for managing subjects in the ERP system.
/// This service handles academic subjects and courses, including creation, modification, and removal of subject records.
/// </summary>
[ServiceContract]
public interface ISubjectService
{
    /// <summary>
    /// Retrieves a list of subjects with pagination support.
    /// Use this endpoint to get multiple subject records with optional pagination.
    /// </summary>
    /// <param name="limit">Maximum number of subjects to retrieve (default: 100, maximum: 1000)</param>
    /// <param name="offset">Number of subjects to skip for pagination (default: 0)</param>
    /// <returns>A collection of Subject objects</returns>
    /// <example>
    /// <code>
    /// // Get first 10 subjects
    /// var subjects = await ListAsync(10, 0);
    /// </code>
    /// </example>
    [OperationContract]
    [WebGet(UriTemplate = "/subjects?limit={limit}&offset={offset}", ResponseFormat = WebMessageFormat.Json)]
    Task<IEnumerable<Subject>> ListAsync(int limit = 100, int offset = 0);

    /// <summary>
    /// Retrieves a specific subject by its unique identifier.
    /// Use this endpoint to get detailed information about a specific subject.
    /// </summary>
    /// <param name="subject_code">The unique identifier of the subject record (must be greater than 0)</param>
    /// <returns>The Subject object if found, null otherwise</returns>
    /// <example>
    /// <code>
    /// // Get subject with code 1
    /// var subject = await GetAsync("1");
    /// </code>
    /// </example>
    [OperationContract]
    [WebGet(UriTemplate = "/subjects/{subject_code}", ResponseFormat = WebMessageFormat.Json)]
    Task<Subject?> GetAsync(string subject_code);

    /// <summary>
    /// Creates a new subject record in the system.
    /// Use this endpoint to add a new academic subject to the institution.
    /// </summary>
    /// <param name="item">The subject object to create. The subject_code field is ignored and will be assigned automatically.</param>
    /// <returns>The created Subject object with assigned code</returns>
    /// <example>
    /// <code>
    /// // Create a new subject
    /// var newSubject = new Subject 
    /// {
    ///     course_id = 101,
    ///     subject_name = "Data Structures",
    ///     credits = 3
    /// };
    /// var createdSubject = await CreateAsync(newSubject);
    /// </code>
    /// </example>
    [OperationContract]
    [WebInvoke(Method = "POST", UriTemplate = "/subjects", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
    Task<Subject> CreateAsync(Subject item);

    /// <summary>
    /// Updates an existing subject record with new information.
    /// Use this endpoint to modify an existing subject, such as updating the name or credit hours.
    /// </summary>
    /// <param name="subject_code">The unique identifier of the subject to update (must match the code in the item parameter)</param>
    /// <param name="item">The subject object with updated information</param>
    /// <returns>The updated Subject object if successful, null otherwise</returns>
    /// <example>
    /// <code>
    /// // Update subject with code 1
    /// var updatedSubject = new Subject 
    /// {
    ///     subject_code = 1,
    ///     course_id = 101,
    ///     subject_name = "Advanced Data Structures",
    ///     credits = 4
    /// };
    /// var result = await UpdateAsync("1", updatedSubject);
    /// </code>
    /// </example>
    [OperationContract]
    [WebInvoke(Method = "PUT", UriTemplate = "/subjects/{subject_code}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
    Task<Subject?> UpdateAsync(string subject_code, Subject item);

    /// <summary>
    /// Removes a subject record from the system by its unique identifier.
    /// Use this endpoint to delete a subject, typically used when courses are revised or discontinued.
    /// </summary>
    /// <param name="subject_code">The unique identifier of the subject to remove (must be greater than 0)</param>
    /// <returns>The removed Subject object if successful, null otherwise</returns>
    /// <example>
    /// <code>
    /// // Remove subject with code 1
    /// var removedSubject = await RemoveAsync("1");
    /// </code>
    /// </example>
    [OperationContract]
    [WebInvoke(Method = "DELETE", UriTemplate = "/subjects/{subject_code}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
    Task<Subject?> RemoveAsync(string subject_code);
}