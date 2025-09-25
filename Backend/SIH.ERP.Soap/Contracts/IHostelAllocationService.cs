using CoreWCF;
using CoreWCF.Web;
using SIH.ERP.Soap.Models;

namespace SIH.ERP.Soap.Contracts;

/// <summary>
/// Service contract for managing hostel allocations in the ERP system.
/// This service handles the assignment of students to hostel rooms and tracks allocation periods.
/// </summary>
[ServiceContract]
public interface IHostelAllocationService
{
    /// <summary>
    /// Retrieves a list of hostel allocations with pagination support.
    /// Use this endpoint to get multiple allocation records with optional pagination.
    /// </summary>
    /// <param name="limit">Maximum number of allocations to retrieve (default: 100, maximum: 1000)</param>
    /// <param name="offset">Number of allocations to skip for pagination (default: 0)</param>
    /// <returns>A collection of HostelAllocation objects</returns>
    /// <example>
    /// <code>
    /// // Get first 10 allocations
    /// var allocations = await ListAsync(10, 0);
    /// </code>
    /// </example>
    [OperationContract]
    [WebGet(UriTemplate = "/hostelallocations?limit={limit}&offset={offset}", ResponseFormat = WebMessageFormat.Json)]
    Task<IEnumerable<HostelAllocation>> ListAsync(int limit = 100, int offset = 0);

    /// <summary>
    /// Retrieves a specific hostel allocation by its unique identifier.
    /// Use this endpoint to get detailed information about a specific room allocation.
    /// </summary>
    /// <param name="allocation_id">The unique identifier of the allocation record (must be greater than 0)</param>
    /// <returns>The HostelAllocation object if found, null otherwise</returns>
    /// <example>
    /// <code>
    /// // Get allocation with ID 1
    /// var allocation = await GetAsync("1");
    /// </code>
    /// </example>
    [OperationContract]
    [WebGet(UriTemplate = "/hostelallocations/{allocation_id}", ResponseFormat = WebMessageFormat.Json)]
    Task<HostelAllocation?> GetAsync(string allocation_id);

    /// <summary>
    /// Creates a new hostel allocation record in the system.
    /// Use this endpoint to assign a student to a hostel room for a specific period.
    /// </summary>
    /// <param name="item">The allocation object to create. The allocation_id field is ignored and will be assigned automatically.</param>
    /// <returns>The created HostelAllocation object with assigned ID</returns>
    /// <example>
    /// <code>
    /// // Create a new allocation
    /// var newAllocation = new HostelAllocation 
    /// {
    ///     student_id = 1,
    ///     hostel_id = 1,
    ///     room_id = 101,
    ///     start_date = new DateTime(2025, 9, 1),
    ///     end_date = new DateTime(2026, 8, 31),
    ///     status = "Active"
    /// };
    /// var createdAllocation = await CreateAsync(newAllocation);
    /// </code>
    /// </example>
    [OperationContract]
    [WebInvoke(Method = "POST", UriTemplate = "/hostelallocations", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
    Task<HostelAllocation> CreateAsync(HostelAllocation item);

    /// <summary>
    /// Updates an existing hostel allocation record with new information.
    /// Use this endpoint to modify an existing allocation, such as extending the period or changing status.
    /// </summary>
    /// <param name="allocation_id">The unique identifier of the allocation to update (must match the ID in the item parameter)</param>
    /// <param name="item">The allocation object with updated information</param>
    /// <returns>The updated HostelAllocation object if successful, null otherwise</returns>
    /// <example>
    /// <code>
    /// // Update allocation with ID 1
    /// var updatedAllocation = new HostelAllocation 
    /// {
    ///     allocation_id = 1,
    ///     student_id = 1,
    ///     hostel_id = 1,
    ///     room_id = 101,
    ///     start_date = new DateTime(2025, 9, 1),
    ///     end_date = new DateTime(2026, 12, 31),
    ///     status = "Extended"
    /// };
    /// var result = await UpdateAsync("1", updatedAllocation);
    /// </code>
    /// </example>
    [OperationContract]
    [WebInvoke(Method = "PUT", UriTemplate = "/hostelallocations/{allocation_id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
    Task<HostelAllocation?> UpdateAsync(string allocation_id, HostelAllocation item);

    /// <summary>
    /// Removes a hostel allocation record from the system by its unique identifier.
    /// Use this endpoint to delete an allocation record, typically used when students vacate rooms.
    /// </summary>
    /// <param name="allocation_id">The unique identifier of the allocation to remove (must be greater than 0)</param>
    /// <returns>The removed HostelAllocation object if successful, null otherwise</returns>
    /// <example>
    /// <code>
    /// // Remove allocation with ID 1
    /// var removedAllocation = await RemoveAsync("1");
    /// </code>
    /// </example>
    [OperationContract]
    [WebInvoke(Method = "DELETE", UriTemplate = "/hostelallocations/{allocation_id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
    Task<HostelAllocation?> RemoveAsync(string allocation_id);
}