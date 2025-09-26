using CoreWCF;
using CoreWCF.Web;
using SIH.ERP.Soap.Models;

namespace SIH.ERP.Soap.Contracts;

/// <summary>
/// Service contract for managing student admissions in the ERP system.
/// This service handles the entire admission process from application to confirmation.
/// </summary>
[ServiceContract]
public interface IAdmissionService
{
    /// <summary>
    /// Retrieves a list of admissions with pagination support.
    /// Use this endpoint to get multiple admission records with optional pagination.
    /// </summary>
    /// <param name="limit">Maximum number of admissions to retrieve (default: 100, maximum: 1000)</param>
    /// <param name="offset">Number of admissions to skip for pagination (default: 0)</param>
    /// <returns>A collection of Admission objects</returns>
    [OperationContract]
    [WebGet(UriTemplate = "/admissions?limit={limit}&offset={offset}", ResponseFormat = WebMessageFormat.Json)]
    Task<IEnumerable<Admission>> ListAsync(int limit = 100, int offset = 0);

    /// <summary>
    /// Retrieves a specific admission record by its unique identifier.
    /// Use this endpoint to get detailed information about a specific admission.
    /// </summary>
    /// <param name="admission_id">The unique identifier of the admission record (must be greater than 0)</param>
    /// <returns>The Admission object if found, null otherwise</returns>
    [OperationContract]
    [WebGet(UriTemplate = "/admissions/{admission_id}", ResponseFormat = WebMessageFormat.Json)]
    Task<Admission?> GetAsync(string admission_id);

    /// <summary>
    /// Creates a new admission record in the system.
    /// Use this endpoint to add a new student admission application.
    /// </summary>
    /// <param name="item">The admission object to create. The admission_id field is ignored and will be assigned automatically.</param>
    /// <returns>The created Admission object with assigned ID</returns>
    [OperationContract]
    [WebInvoke(Method = "POST", UriTemplate = "/admissions", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
    Task<Admission> CreateAsync(Admission item);

    /// <summary>
    /// Updates an existing admission record with new information.
    /// Use this endpoint to modify an existing admission record, such as updating verification status.
    /// </summary>
    /// <param name="admission_id">The unique identifier of the admission to update (must match the ID in the item parameter)</param>
    /// <param name="item">The admission object with updated information</param>
    /// <returns>The updated Admission object if successful, null otherwise</returns>
    [OperationContract]
    [WebInvoke(Method = "PUT", UriTemplate = "/admissions/{admission_id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
    Task<Admission?> UpdateAsync(string admission_id, Admission item);

    /// <summary>
    /// Removes an admission record from the system by its unique identifier.
    /// Use this endpoint to delete an admission record, typically used for data cleanup.
    /// </summary>
    /// <param name="admission_id">The unique identifier of the admission to remove (must be greater than 0)</param>
    /// <returns>The removed Admission object if successful, null otherwise</returns>
    [OperationContract]
    [WebInvoke(Method = "DELETE", UriTemplate = "/admissions/{admission_id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
    Task<Admission?> RemoveAsync(string admission_id);
}