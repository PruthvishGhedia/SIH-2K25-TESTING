using CoreWCF;
using CoreWCF.Web;
using SIH.ERP.Soap.Models;

namespace SIH.ERP.Soap.Contracts;

/// <summary>
/// Service contract for managing student fees in the ERP system.
/// This service handles fee structures, payments, and payment tracking for students.
/// </summary>
[ServiceContract]
public interface IFeesService
{
    /// <summary>
    /// Retrieves a list of fee records with pagination support.
    /// Use this endpoint to get multiple fee records with optional pagination.
    /// </summary>
    /// <param name="limit">Maximum number of fee records to retrieve (default: 100, maximum: 1000)</param>
    /// <param name="offset">Number of fee records to skip for pagination (default: 0)</param>
    /// <returns>A collection of Fees objects</returns>
    [OperationContract]
    [WebGet(UriTemplate = "/fees?limit={limit}&offset={offset}", ResponseFormat = WebMessageFormat.Json)]
    Task<IEnumerable<Fees>> ListAsync(int limit = 100, int offset = 0);

    /// <summary>
    /// Retrieves a specific fee record by its unique identifier.
    /// Use this endpoint to get detailed information about a specific fee record.
    /// </summary>
    /// <param name="fee_id">The unique identifier of the fee record (must be greater than 0)</param>
    /// <returns>The Fees object if found, null otherwise</returns>
    [OperationContract]
    [WebGet(UriTemplate = "/fees/{fee_id}", ResponseFormat = WebMessageFormat.Json)]
    Task<Fees?> GetAsync(string fee_id);

    /// <summary>
    /// Creates a new fee record in the system.
    /// Use this endpoint to add a new fee record for a student.
    /// </summary>
    /// <param name="item">The fees object to create. The fee_id field is ignored and will be assigned automatically.</param>
    /// <returns>The created Fees object with assigned ID</returns>
    [OperationContract]
    [WebInvoke(Method = "POST", UriTemplate = "/fees", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
    Task<Fees> CreateAsync(Fees item);

    /// <summary>
    /// Updates an existing fee record with new information.
    /// Use this endpoint to modify an existing fee record, such as updating payment status.
    /// </summary>
    /// <param name="fee_id">The unique identifier of the fee record to update (must match the ID in the item parameter)</param>
    /// <param name="item">The fees object with updated information</param>
    /// <returns>The updated Fees object if successful, null otherwise</returns>
    [OperationContract]
    [WebInvoke(Method = "PUT", UriTemplate = "/fees/{fee_id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
    Task<Fees?> UpdateAsync(string fee_id, Fees item);

    /// <summary>
    /// Removes a fee record from the system by its unique identifier.
    /// Use this endpoint to delete a fee record, typically used for data correction.
    /// </summary>
    /// <param name="fee_id">The unique identifier of the fee record to remove (must be greater than 0)</param>
    /// <returns>The removed Fees object if successful, null otherwise</returns>
    [OperationContract]
    [WebInvoke(Method = "DELETE", UriTemplate = "/fees/{fee_id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
    Task<Fees?> RemoveAsync(string fee_id);
}