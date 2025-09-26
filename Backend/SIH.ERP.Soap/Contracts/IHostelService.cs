using CoreWCF;
using CoreWCF.Web;
using SIH.ERP.Soap.Models;

namespace SIH.ERP.Soap.Contracts;

/// <summary>
/// Service contract for managing hostels in the ERP system.
/// This service handles hostel facilities, including creation, modification, and removal of hostel records.
/// </summary>
[ServiceContract]
public interface IHostelService
{
    /// <summary>
    /// Retrieves a list of hostels with pagination support.
    /// Use this endpoint to get multiple hostel records with optional pagination.
    /// </summary>
    /// <param name="limit">Maximum number of hostels to retrieve (default: 100, maximum: 1000)</param>
    /// <param name="offset">Number of hostels to skip for pagination (default: 0)</param>
    /// <returns>A collection of Hostel objects</returns>
    /// <example>
    /// <code>
    /// // Get first 10 hostels
    /// var hostels = await ListAsync(10, 0);
    /// </code>
    /// </example>
    [OperationContract]
    [WebGet(UriTemplate = "/hostels?limit={limit}&offset={offset}", ResponseFormat = WebMessageFormat.Json)]
    Task<IEnumerable<Hostel>> ListAsync(int limit = 100, int offset = 0);

    /// <summary>
    /// Retrieves a specific hostel record by its unique identifier.
    /// Use this endpoint to get detailed information about a specific hostel.
    /// </summary>
    /// <param name="hostel_id">The unique identifier of the hostel record (must be greater than 0)</param>
    /// <returns>The Hostel object if found, null otherwise</returns>
    /// <example>
    /// <code>
    /// // Get hostel with ID 1
    /// var hostel = await GetAsync("1");
    /// </code>
    /// </example>
    [OperationContract]
    [WebGet(UriTemplate = "/hostels/{hostel_id}", ResponseFormat = WebMessageFormat.Json)]
    Task<Hostel?> GetAsync(string hostel_id);

    /// <summary>
    /// Creates a new hostel record in the system.
    /// Use this endpoint to add a new hostel facility to the institution.
    /// </summary>
    /// <param name="item">The hostel object to create. The hostel_id field is ignored and will be assigned automatically.</param>
    /// <returns>The created Hostel object with assigned ID</returns>
    /// <example>
    /// <code>
    /// // Create a new hostel
    /// var newHostel = new Hostel 
    /// {
    ///     hostel_name = "Boys Hostel A",
    ///     type = "Boys"
    /// };
    /// var createdHostel = await CreateAsync(newHostel);
    /// </code>
    /// </example>
    [OperationContract]
    [WebInvoke(Method = "POST", UriTemplate = "/hostels", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
    Task<Hostel> CreateAsync(Hostel item);

    /// <summary>
    /// Updates an existing hostel record with new information.
    /// Use this endpoint to modify an existing hostel record, such as renaming or changing type.
    /// </summary>
    /// <param name="hostel_id">The unique identifier of the hostel to update (must match the ID in the item parameter)</param>
    /// <param name="item">The hostel object with updated information</param>
    /// <returns>The updated Hostel object if successful, null otherwise</returns>
    /// <example>
    /// <code>
    /// // Update hostel with ID 1
    /// var updatedHostel = new Hostel 
    /// {
    ///     hostel_id = 1,
    ///     hostel_name = "Boys Hostel Block A",
    ///     type = "Boys"
    /// };
    /// var result = await UpdateAsync("1", updatedHostel);
    /// </code>
    /// </example>
    [OperationContract]
    [WebInvoke(Method = "PUT", UriTemplate = "/hostels/{hostel_id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
    Task<Hostel?> UpdateAsync(string hostel_id, Hostel item);

    /// <summary>
    /// Removes a hostel record from the system by its unique identifier.
    /// Use this endpoint to delete a hostel record, typically used when facilities are decommissioned.
    /// </summary>
    /// <param name="hostel_id">The unique identifier of the hostel to remove (must be greater than 0)</param>
    /// <returns>The removed Hostel object if successful, null otherwise</returns>
    /// <example>
    /// <code>
    /// // Remove hostel with ID 1
    /// var removedHostel = await RemoveAsync("1");
    /// </code>
    /// </example>
    [OperationContract]
    [WebInvoke(Method = "DELETE", UriTemplate = "/hostels/{hostel_id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
    Task<Hostel?> RemoveAsync(string hostel_id);
}