using CoreWCF;
using CoreWCF.Web;
using SIH.ERP.Soap.Models;

namespace SIH.ERP.Soap.Contracts;

/// <summary>
/// Service contract for managing guardians in the ERP system.
/// This service handles guardian and parent information for students, including contact details and relationship data.
/// </summary>
[ServiceContract]
public interface IGuardianService
{
    /// <summary>
    /// Retrieves a list of guardians with pagination support.
    /// Use this endpoint to get multiple guardian records with optional pagination.
    /// </summary>
    /// <param name="limit">Maximum number of guardians to retrieve (default: 100, maximum: 1000)</param>
    /// <param name="offset">Number of guardians to skip for pagination (default: 0)</param>
    /// <returns>A collection of Guardian objects</returns>
    [OperationContract]
    [WebGet(UriTemplate = "/guardians?limit={limit}&offset={offset}", ResponseFormat = WebMessageFormat.Json)]
    Task<IEnumerable<Guardian>> ListAsync(int limit = 100, int offset = 0);

    /// <summary>
    /// Retrieves a specific guardian by its unique identifier.
    /// Use this endpoint to get detailed information about a specific guardian.
    /// </summary>
    /// <param name="guardian_id">The unique identifier of the guardian record (must be greater than 0)</param>
    /// <returns>The Guardian object if found, null otherwise</returns>
    [OperationContract]
    [WebGet(UriTemplate = "/guardians/{guardian_id}", ResponseFormat = WebMessageFormat.Json)]
    Task<Guardian?> GetAsync(string guardian_id);

    /// <summary>
    /// Creates a new guardian record in the system.
    /// Use this endpoint to add a new guardian or parent information for a student.
    /// </summary>
    /// <param name="item">The guardian object to create. The guardian_id field is ignored and will be assigned automatically.</param>
    /// <returns>The created Guardian object with assigned ID</returns>
    [OperationContract]
    [WebInvoke(Method = "POST", UriTemplate = "/guardians", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
    Task<Guardian> CreateAsync(Guardian item);

    /// <summary>
    /// Updates an existing guardian record with new information.
    /// Use this endpoint to modify an existing guardian record, such as updating contact information.
    /// </summary>
    /// <param name="guardian_id">The unique identifier of the guardian to update (must match the ID in the item parameter)</param>
    /// <param name="item">The guardian object with updated information</param>
    /// <returns>The updated Guardian object if successful, null otherwise</returns>
    [OperationContract]
    [WebInvoke(Method = "PUT", UriTemplate = "/guardians/{guardian_id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
    Task<Guardian?> UpdateAsync(string guardian_id, Guardian item);

    /// <summary>
    /// Removes a guardian record from the system by its unique identifier.
    /// Use this endpoint to delete a guardian record, typically used for data correction or privacy compliance.
    /// </summary>
    /// <param name="guardian_id">The unique identifier of the guardian to remove (must be greater than 0)</param>
    /// <returns>The removed Guardian object if successful, null otherwise</returns>
    [OperationContract]
    [WebInvoke(Method = "DELETE", UriTemplate = "/guardians/{guardian_id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
    Task<Guardian?> RemoveAsync(string guardian_id);
}