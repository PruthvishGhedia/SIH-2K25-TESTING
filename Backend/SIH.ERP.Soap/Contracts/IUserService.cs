using CoreWCF;
using CoreWCF.Web;
using SIH.ERP.Soap.Models;

namespace SIH.ERP.Soap.Contracts;

/// <summary>
/// Service contract for managing users in the ERP system.
/// This service handles user accounts, authentication, and user-related information management.
/// </summary>
[ServiceContract]
public interface IUserService
{
    /// <summary>
    /// Retrieves a list of users with pagination support.
    /// Use this endpoint to get multiple user records with optional pagination.
    /// </summary>
    /// <param name="limit">Maximum number of users to retrieve (default: 100, maximum: 1000)</param>
    /// <param name="offset">Number of users to skip for pagination (default: 0)</param>
    /// <returns>A collection of User objects</returns>
    /// <example>
    /// <code>
    /// // Get first 10 users
    /// var users = await ListAsync(10, 0);
    /// </code>
    /// </example>
    [OperationContract]
    [WebGet(UriTemplate = "/users?limit={limit}&offset={offset}", ResponseFormat = WebMessageFormat.Json)]
    Task<IEnumerable<User>> ListAsync(int limit = 100, int offset = 0);

    /// <summary>
    /// Retrieves a specific user by its unique identifier.
    /// Use this endpoint to get detailed information about a specific user account.
    /// </summary>
    /// <param name="user_id">The unique identifier of the user record (must be greater than 0)</param>
    /// <returns>The User object if found, null otherwise</returns>
    /// <example>
    /// <code>
    /// // Get user with ID 1
    /// var user = await GetAsync("1");
    /// </code>
    /// </example>
    [OperationContract]
    [WebGet(UriTemplate = "/users/{user_id}", ResponseFormat = WebMessageFormat.Json)]
    Task<User?> GetAsync(string user_id);

    /// <summary>
    /// Creates a new user record in the system.
    /// Use this endpoint to register a new user account in the system.
    /// </summary>
    /// <param name="item">The user object to create. The user_id field is ignored and will be assigned automatically.</param>
    /// <returns>The created User object with assigned ID</returns>
    /// <example>
    /// <code>
    /// // Create a new user
    /// var newUser = new User 
    /// {
    ///     full_name = "John Doe",
    ///     email = "john.doe@example.com",
    ///     dob = new DateTime(1990, 1, 1),
    ///     password_hash = "hashed_password_here",
    ///     is_active = true
    /// };
    /// var createdUser = await CreateAsync(newUser);
    /// </code>
    /// </example>
    [OperationContract]
    [WebInvoke(Method = "POST", UriTemplate = "/users", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
    Task<User> CreateAsync(User item);

    /// <summary>
    /// Updates an existing user record with new information.
    /// Use this endpoint to modify an existing user account, such as updating personal information or account status.
    /// </summary>
    /// <param name="user_id">The unique identifier of the user to update (must match the ID in the item parameter)</param>
    /// <param name="item">The user object with updated information</param>
    /// <returns>The updated User object if successful, null otherwise</returns>
    /// <example>
    /// <code>
    /// // Update user with ID 1
    /// var updatedUser = new User 
    /// {
    ///     user_id = 1,
    ///     full_name = "John Doe Updated",
    ///     email = "john.doe.updated@example.com",
    ///     dob = new DateTime(1990, 1, 1),
    ///     password_hash = "new_hashed_password_here",
    ///     is_active = true
    /// };
    /// var result = await UpdateAsync("1", updatedUser);
    /// </code>
    /// </example>
    [OperationContract]
    [WebInvoke(Method = "PUT", UriTemplate = "/users/{user_id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
    Task<User?> UpdateAsync(string user_id, User item);

    /// <summary>
    /// Removes a user record from the system by its unique identifier.
    /// Use this endpoint to delete a user account, typically used for data privacy compliance or account termination.
    /// </summary>
    /// <param name="user_id">The unique identifier of the user to remove (must be greater than 0)</param>
    /// <returns>The removed User object if successful, null otherwise</returns>
    /// <example>
    /// <code>
    /// // Remove user with ID 1
    /// var removedUser = await RemoveAsync("1");
    /// </code>
    /// </example>
    [OperationContract]
    [WebInvoke(Method = "DELETE", UriTemplate = "/users/{user_id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
    Task<User?> RemoveAsync(string user_id);
}