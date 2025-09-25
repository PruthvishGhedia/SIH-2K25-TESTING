using CoreWCF;
using SIH.ERP.Soap.Models;

namespace SIH.ERP.Soap.Contracts;

/// <summary>
/// Service contract for managing roles in the ERP system
/// </summary>
[ServiceContract]
public interface IRoleService
{
    /// <summary>
    /// Retrieves a list of roles with pagination
    /// </summary>
    /// <param name="limit">Maximum number of roles to retrieve (default: 100)</param>
    /// <param name="offset">Number of roles to skip (default: 0)</param>
    /// <returns>A collection of Role objects</returns>
    [OperationContract]
    Task<IEnumerable<Role>> ListAsync(int limit = 100, int offset = 0);

    /// <summary>
    /// Retrieves a specific role by ID
    /// </summary>
    /// <param name="role_id">The unique identifier of the role</param>
    /// <returns>The Role object if found, null otherwise</returns>
    [OperationContract]
    Task<Role?> GetAsync(int role_id);

    /// <summary>
    /// Creates a new role record
    /// </summary>
    /// <param name="item">The role object to create</param>
    /// <returns>The created Role object with assigned ID</returns>
    [OperationContract]
    Task<Role> CreateAsync(Role item);

    /// <summary>
    /// Updates an existing role record
    /// </summary>
    /// <param name="role_id">The unique identifier of the role to update</param>
    /// <param name="item">The role object with updated information</param>
    /// <returns>The updated Role object if successful, null otherwise</returns>
    [OperationContract]
    Task<Role?> UpdateAsync(int role_id, Role item);

    /// <summary>
    /// Removes a role record by ID
    /// </summary>
    /// <param name="role_id">The unique identifier of the role to remove</param>
    /// <returns>The removed Role object if successful, null otherwise</returns>
    [OperationContract]
    Task<Role?> RemoveAsync(int role_id);
}