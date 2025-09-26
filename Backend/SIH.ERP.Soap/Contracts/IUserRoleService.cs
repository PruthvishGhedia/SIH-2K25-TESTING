using CoreWCF;
using CoreWCF.Web;
using SIH.ERP.Soap.Models;

namespace SIH.ERP.Soap.Contracts;

/// <summary>
/// Service contract for managing user-role assignments in the ERP system.
/// This service handles the mapping between users and their assigned roles for access control.
/// </summary>
[ServiceContract]
public interface IUserRoleService
{
    /// <summary>
    /// Retrieves a list of user-role assignments with pagination support.
    /// Use this endpoint to get multiple user-role mapping records with optional pagination.
    /// </summary>
    /// <param name="limit">Maximum number of user-role assignments to retrieve (default: 100, maximum: 1000)</param>
    /// <param name="offset">Number of user-role assignments to skip for pagination (default: 0)</param>
    /// <returns>A collection of UserRole objects</returns>
    /// <example>
    /// <code>
    /// // Get first 10 user-role assignments
    /// var userRoles = await ListAsync(10, 0);
    /// </code>
    /// </example>
    [OperationContract]
    [WebGet(UriTemplate = "/userroles?limit={limit}&offset={offset}", ResponseFormat = WebMessageFormat.Json)]
    Task<IEnumerable<UserRole>> ListAsync(int limit = 100, int offset = 0);

    /// <summary>
    /// Retrieves a specific user-role assignment record by its unique identifier.
    /// Use this endpoint to get detailed information about a specific user-role mapping.
    /// </summary>
    /// <param name="user_role_id">The unique identifier of the user-role assignment record (must be greater than 0)</param>
    /// <returns>The UserRole object if found, null otherwise</returns>
    /// <example>
    /// <code>
    /// // Get user-role assignment with ID 1
    /// var userRole = await GetAsync("1");
    /// </code>
    /// </example>
    [OperationContract]
    [WebGet(UriTemplate = "/userroles/{user_role_id}", ResponseFormat = WebMessageFormat.Json)]
    Task<UserRole?> GetAsync(string user_role_id);

    /// <summary>
    /// Creates a new user-role assignment record in the system.
    /// Use this endpoint to assign a role to a user for access control purposes.
    /// </summary>
    /// <param name="item">The user-role object to create. The user_role_id field is ignored and will be assigned automatically.</param>
    /// <returns>The created UserRole object with assigned ID</returns>
    /// <example>
    /// <code>
    /// // Create a new user-role assignment
    /// var newUserRole = new UserRole 
    /// {
    ///     user_id = 1,
    ///     role_id = 2
    /// };
    /// var createdUserRole = await CreateAsync(newUserRole);
    /// </code>
    /// </example>
    [OperationContract]
    [WebInvoke(Method = "POST", UriTemplate = "/userroles", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
    Task<UserRole> CreateAsync(UserRole item);

    /// <summary>
    /// Updates an existing user-role assignment record with new information.
    /// Use this endpoint to modify an existing user-role mapping, such as changing the assigned role.
    /// </summary>
    /// <param name="user_role_id">The unique identifier of the user-role assignment to update (must match the ID in the item parameter)</param>
    /// <param name="item">The user-role object with updated information</param>
    /// <returns>The updated UserRole object if successful, null otherwise</returns>
    /// <example>
    /// <code>
    /// // Update user-role assignment with ID 1
    /// var updatedUserRole = new UserRole 
    /// {
    ///     user_role_id = 1,
    ///     user_id = 1,
    ///     role_id = 3
    /// };
    /// var result = await UpdateAsync("1", updatedUserRole);
    /// </code>
    /// </example>
    [OperationContract]
    [WebInvoke(Method = "PUT", UriTemplate = "/userroles/{user_role_id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
    Task<UserRole?> UpdateAsync(string user_role_id, UserRole item);

    /// <summary>
    /// Removes a user-role assignment record from the system by its unique identifier.
    /// Use this endpoint to delete a user-role mapping, typically used when roles are revoked.
    /// </summary>
    /// <param name="user_role_id">The unique identifier of the user-role assignment to remove (must be greater than 0)</param>
    /// <returns>The removed UserRole object if successful, null otherwise</returns>
    /// <example>
    /// <code>
    /// // Remove user-role assignment with ID 1
    /// var removedUserRole = await RemoveAsync("1");
    /// </code>
    /// </example>
    [OperationContract]
    [WebInvoke(Method = "DELETE", UriTemplate = "/userroles/{user_role_id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
    Task<UserRole?> RemoveAsync(string user_role_id);
}