using CoreWCF;
using CoreWCF.Web;
using SIH.ERP.Soap.Models;

namespace SIH.ERP.Soap.Contracts;

/// <summary>
/// Service contract for managing examination results in the ERP system.
/// This service handles student exam results, marks, and grades for academic assessment.
/// </summary>
[ServiceContract]
public interface IResultService
{
    /// <summary>
    /// Retrieves a list of examination results with pagination support.
    /// Use this endpoint to get multiple result records with optional pagination.
    /// </summary>
    /// <param name="limit">Maximum number of results to retrieve (default: 100, maximum: 1000)</param>
    /// <param name="offset">Number of results to skip for pagination (default: 0)</param>
    /// <returns>A collection of Result objects</returns>
    /// <example>
    /// <code>
    /// // Get first 10 results
    /// var results = await ListAsync(10, 0);
    /// </code>
    /// </example>
    [OperationContract]
    [WebGet(UriTemplate = "/results?limit={limit}&offset={offset}", ResponseFormat = WebMessageFormat.Json)]
    Task<IEnumerable<Result>> ListAsync(int limit = 100, int offset = 0);

    /// <summary>
    /// Retrieves a specific examination result record by its unique identifier.
    /// Use this endpoint to get detailed information about a specific student's examination performance.
    /// </summary>
    /// <param name="result_id">The unique identifier of the result record (must be greater than 0)</param>
    /// <returns>The Result object if found, null otherwise</returns>
    /// <example>
    /// <code>
    /// // Get result with ID 1
    /// var result = await GetAsync("1");
    /// </code>
    /// </example>
    [OperationContract]
    [WebGet(UriTemplate = "/results/{result_id}", ResponseFormat = WebMessageFormat.Json)]
    Task<Result?> GetAsync(string result_id);

    /// <summary>
    /// Creates a new examination result record in the system.
    /// Use this endpoint to record a student's performance in an examination.
    /// </summary>
    /// <param name="item">The result object to create. The result_id field is ignored and will be assigned automatically.</param>
    /// <returns>The created Result object with assigned ID</returns>
    /// <example>
    /// <code>
    /// // Create a new result
    /// var newResult = new Result 
    /// {
    ///     exam_id = 1,
    ///     student_id = 101,
    ///     marks = 85,
    ///     grade = "A"
    /// };
    /// var createdResult = await CreateAsync(newResult);
    /// </code>
    /// </example>
    [OperationContract]
    [WebInvoke(Method = "POST", UriTemplate = "/results", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
    Task<Result> CreateAsync(Result item);

    /// <summary>
    /// Updates an existing examination result record with new information.
    /// Use this endpoint to modify an existing result, such as correcting marks or grades.
    /// </summary>
    /// <param name="result_id">The unique identifier of the result to update (must match the ID in the item parameter)</param>
    /// <param name="item">The result object with updated information</param>
    /// <returns>The updated Result object if successful, null otherwise</returns>
    /// <example>
    /// <code>
    /// // Update result with ID 1
    /// var updatedResult = new Result 
    /// {
    ///     result_id = 1,
    ///     exam_id = 1,
    ///     student_id = 101,
    ///     marks = 88,
    ///     grade = "A"
    /// };
    /// var result = await UpdateAsync("1", updatedResult);
    /// </code>
    /// </example>
    [OperationContract]
    [WebInvoke(Method = "PUT", UriTemplate = "/results/{result_id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
    Task<Result?> UpdateAsync(string result_id, Result item);

    /// <summary>
    /// Removes an examination result record from the system by its unique identifier.
    /// Use this endpoint to delete a result record, typically used for data correction.
    /// </summary>
    /// <param name="result_id">The unique identifier of the result to remove (must be greater than 0)</param>
    /// <returns>The removed Result object if successful, null otherwise</returns>
    /// <example>
    /// <code>
    /// // Remove result with ID 1
    /// var removedResult = await RemoveAsync("1");
    /// </code>
    /// </example>
    [OperationContract]
    [WebInvoke(Method = "DELETE", UriTemplate = "/results/{result_id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
    Task<Result?> RemoveAsync(string result_id);
}