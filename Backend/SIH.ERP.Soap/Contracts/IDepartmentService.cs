using CoreWCF;
using CoreWCF.Web;
using SIH.ERP.Soap.Models;

namespace SIH.ERP.Soap.Contracts;

/// <summary>
/// Service contract for managing departments in the ERP system.
/// This service handles academic and administrative departments, including creation, modification, and removal of department records.
/// </summary>
[ServiceContract]
public interface IDepartmentService
{
    /// <summary>
    /// Retrieves a list of departments with pagination support.
    /// Use this endpoint to get multiple department records with optional pagination.
    /// </summary>
    /// <param name="limit">Maximum number of departments to retrieve (default: 100, maximum: 1000)</param>
    /// <param name="offset">Number of departments to skip for pagination (default: 0)</param>
    /// <returns>A collection of Department objects</returns>
    [OperationContract]
    [WebGet(UriTemplate = "/departments?limit={limit}&offset={offset}", ResponseFormat = WebMessageFormat.Json)]
    Task<IEnumerable<Department>> ListAsync(int limit = 100, int offset = 0);

    /// <summary>
    /// Retrieves a specific department by its unique identifier.
    /// Use this endpoint to get detailed information about a specific department.
    /// </summary>
    /// <param name="dept_id">The unique identifier of the department record (must be greater than 0)</param>
    /// <returns>The Department object if found, null otherwise</returns>
    [OperationContract]
    [WebGet(UriTemplate = "/departments/{dept_id}", ResponseFormat = WebMessageFormat.Json)]
    Task<Department?> GetAsync(string dept_id);

    /// <summary>
    /// Creates a new department record in the system.
    /// Use this endpoint to add a new academic or administrative department to the institution.
    /// </summary>
    /// <param name="item">The department object to create. The dept_id field is ignored and will be assigned automatically.</param>
    /// <returns>The created Department object with assigned ID</returns>
    [OperationContract]
    [WebInvoke(Method = "POST", UriTemplate = "/departments", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
    Task<Department> CreateAsync(Department item);

    /// <summary>
    /// Updates an existing department record with new information.
    /// Use this endpoint to modify an existing department record, such as updating the department head or description.
    /// </summary>
    /// <param name="dept_id">The unique identifier of the department to update (must match the ID in the item parameter)</param>
    /// <param name="item">The department object with updated information</param>
    /// <returns>The updated Department object if successful, null otherwise</returns>
    [OperationContract]
    [WebInvoke(Method = "PUT", UriTemplate = "/departments/{dept_id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
    Task<Department?> UpdateAsync(string dept_id, Department item);

    /// <summary>
    /// Removes a department record from the system by its unique identifier.
    /// Use this endpoint to delete a department record, typically used when departments are restructured or merged.
    /// </summary>
    /// <param name="dept_id">The unique identifier of the department to remove (must be greater than 0)</param>
    /// <returns>The removed Department object if successful, null otherwise</returns>
    [OperationContract]
    [WebInvoke(Method = "DELETE", UriTemplate = "/departments/{dept_id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
    Task<Department?> RemoveAsync(string dept_id);
}