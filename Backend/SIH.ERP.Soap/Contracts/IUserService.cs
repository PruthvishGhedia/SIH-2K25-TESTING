using CoreWCF;
using SIH.ERP.Soap.Models;

namespace SIH.ERP.Soap.Contracts;

/// <summary>
/// Service contract for managing users in the ERP system
/// </summary>
[ServiceContract]
public interface IUserService
{
    /// <summary>
    /// Retrieves a list of users with pagination
    /// </summary>
    /// <param name="limit">Maximum number of users to retrieve (default: 100)</param>
    /// <param name="offset">Number of users to skip (default: 0)</param>
    /// <returns>A collection of User objects</returns>
    [OperationContract]
    Task<IEnumerable<User>> ListAsync(int limit = 100, int offset = 0);

    /// <summary>
    /// Retrieves a specific user by ID
    /// </summary>
    /// <param name="user_id">The unique identifier of the user</param>
    /// <returns>The User object if found, null otherwise</returns>
    [OperationContract]
    Task<User?> GetAsync(int user_id);

    /// <summary>
    /// Creates a new user record
    /// </summary>
    /// <param name="item">The user object to create</param>
    /// <returns>The created User object with assigned ID</returns>
    [OperationContract]
    Task<User> CreateAsync(User item);

    /// <summary>
    /// Updates an existing user record
    /// </summary>
    /// <param name="user_id">The unique identifier of the user to update</param>
    /// <param name="item">The user object with updated information</param>
    /// <returns>The updated User object if successful, null otherwise</returns>
    [OperationContract]
    Task<User?> UpdateAsync(int user_id, User item);

    /// <summary>
    /// Removes a user record by ID
    /// </summary>
    /// <param name="user_id">The unique identifier of the user to remove</param>
    /// <returns>The removed User object if successful, null otherwise</returns>
    [OperationContract]
    Task<User?> RemoveAsync(int user_id);
}