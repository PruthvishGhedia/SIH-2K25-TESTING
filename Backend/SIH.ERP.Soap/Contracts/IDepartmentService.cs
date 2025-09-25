using CoreWCF;
using SIH.ERP.Soap.Models;

namespace SIH.ERP.Soap.Contracts;

/// <summary>
/// Service contract for managing departments in the ERP system
/// </summary>
[ServiceContract]
public interface IDepartmentService
{
    /// <summary>
    /// Retrieves a list of departments with pagination
    /// </summary>
    /// <param name="limit">Maximum number of departments to retrieve (default: 100)</param>
    /// <param name="offset">Number of departments to skip (default: 0)</param>
    /// <returns>A collection of Department objects</returns>
    [OperationContract]
    Task<IEnumerable<Department>> ListAsync(int limit = 100, int offset = 0);

    /// <summary>
    /// Retrieves a specific department by ID
    /// </summary>
    /// <param name="dept_id">The unique identifier of the department</param>
    /// <returns>The Department object if found, null otherwise</returns>
    [OperationContract]
    Task<Department?> GetAsync(int dept_id);

    /// <summary>
    /// Creates a new department record
    /// </summary>
    /// <param name="item">The department object to create</param>
    /// <returns>The created Department object with assigned ID</returns>
    [OperationContract]
    Task<Department> CreateAsync(Department item);

    /// <summary>
    /// Updates an existing department record
    /// </summary>
    /// <param name="dept_id">The unique identifier of the department to update</param>
    /// <param name="item">The department object with updated information</param>
    /// <returns>The updated Department object if successful, null otherwise</returns>
    [OperationContract]
    Task<Department?> UpdateAsync(int dept_id, Department item);

    /// <summary>
    /// Removes a department record by ID
    /// </summary>
    /// <param name="dept_id">The unique identifier of the department to remove</param>
    /// <returns>The removed Department object if successful, null otherwise</returns>
    [OperationContract]
    Task<Department?> RemoveAsync(int dept_id);
}