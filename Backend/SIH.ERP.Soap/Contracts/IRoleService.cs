using CoreWCF;
using CoreWCF.Web;
using SIH.ERP.Soap.Models;

namespace SIH.ERP.Soap.Contracts;

/// <summary>
/// Service contract for managing roles in the ERP system.
/// This service handles user roles and permissions, defining access levels and capabilities within the system.
/// </summary>
[ServiceContract]
public interface IRoleService
{
    /// <summary>
    /// Retrieves a list of roles with pagination support.
    /// Use this endpoint to get multiple role records with optional pagination.
    /// </summary>
    /// <param name="limit">Maximum number of roles to retrieve (default: 100, maximum: 1000)</param>
    /// <param name="offset">Number of roles to skip for pagination (default: 0)</param>
    /// <returns>A collection of Role objects</returns>
    /// <example>
    /// <code>
    /// // Get first 10 roles
    /// var roles = await ListAsync(10, 0);
    /// </code>
    /// </example>
    [OperationContract]
    [WebGet(UriTemplate = "/roles?limit={limit}&offset={offset}", ResponseFormat = WebMessageFormat.Json)]
    Task<IEnumerable<Role>> ListAsync(int limit = 100, int offset = 0);

    /// <summary>
    /// Retrieves a specific role by its unique identifier.
    /// Use this endpoint to get detailed information about a specific role.
    /// </summary>
    /// <param name="role_id">The unique identifier of the role record (must be greater than 0)</param>
    /// <returns>The Role object if found, null otherwise</returns>
    /// <example>
    /// <code>
    /// // Get role with ID 1
    /// var role = await GetAsync("1");
    /// </code>
    /// </example>
    [OperationContract]
    [WebGet(UriTemplate = "/roles/{role_id}", ResponseFormat = WebMessageFormat.Json)]
    Task<Role?> GetAsync(string role_id);

    /// <summary>
    /// Creates a new role record in the system.
    /// Use this endpoint to add a new role with specific permissions to the system.
    /// </summary>
    /// <param name="item">The role object to create. The role_id field is ignored and will be assigned automatically.</param>
    /// <returns>The created Role object with assigned ID</returns>
    /// <example>
    /// <code>
    /// // Create a new role
    /// var newRole = new Role 
    /// {
    ///     role_name = "Administrator",
    ///     role_description = "System administrator with full access"
    /// };
    /// var createdRole = await CreateAsync(newRole);
    /// </code>
    /// </example>
    [OperationContract]
    [WebInvoke(Method = "POST", UriTemplate = "/roles", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
    Task<Role> CreateAsync(Role item);

    /// <summary>
    /// Updates an existing role record with new information.
    /// Use this endpoint to modify an existing role, such as updating the description or permissions.
    /// </summary>
    /// <param name="role_id">The unique identifier of the role to update (must match the ID in the item parameter)</param>
    /// <param name="item">The role object with updated information</param>
    /// <returns>The updated Role object if successful, null otherwise</returns>
    /// <example>
    /// <code>
    /// // Update role with ID 1
    /// var updatedRole = new Role 
    /// {
    ///     role_id = 1,
    ///     role_name = "Super Administrator",
    ///     role_description = "System administrator with full access and security management"
    /// };
    /// var result = await UpdateAsync("1", updatedRole);
    /// </code>
    /// </example>
    [OperationContract]
    [WebInvoke(Method = "PUT", UriTemplate = "/roles/{role_id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
    Task<Role?> UpdateAsync(string role_id, Role item);

    /// <summary>
    /// Removes a role record from the system by its unique identifier.
    /// Use this endpoint to delete a role, typically used when roles are deprecated or restructured.
    /// </summary>
    /// <param name="role_id">The unique identifier of the role to remove (must be greater than 0)</param>
    /// <returns>The removed Role object if successful, null otherwise</returns>
    /// <example>
    /// <code>
    /// // Remove role with ID 1
    /// var removedRole = await RemoveAsync("1");
    /// </code>
    /// </example>
    [OperationContract]
    [WebInvoke(Method = "DELETE", UriTemplate = "/roles/{role_id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
    Task<Role?> RemoveAsync(string role_id);
}