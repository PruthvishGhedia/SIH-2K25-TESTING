using CoreWCF;
using CoreWCF.Web;
using SIH.ERP.Soap.Models;

namespace SIH.ERP.Soap.Contracts;

/// <summary>
/// Service contract for managing book issues in the ERP system.
/// This service handles the lending of library books to students, including issue dates, return dates, and status tracking.
/// </summary>
[ServiceContract]
public interface IBookIssueService
{
    /// <summary>
    /// Retrieves a list of book issues with pagination support.
    /// Use this endpoint to get multiple book issue records with optional pagination.
    /// </summary>
    /// <param name="limit">Maximum number of book issues to retrieve (default: 100, maximum: 1000)</param>
    /// <param name="offset">Number of book issues to skip for pagination (default: 0)</param>
    /// <returns>A collection of BookIssue objects</returns>
    /// <example>
    /// <code>
    /// // Get first 10 book issues
    /// var issues = await ListAsync(10, 0);
    /// </code>
    /// </example>
    [OperationContract]
    [WebGet(UriTemplate = "/bookissues?limit={limit}&offset={offset}", ResponseFormat = WebMessageFormat.Json)]
    Task<IEnumerable<BookIssue>> ListAsync(int limit = 100, int offset = 0);

    /// <summary>
    /// Retrieves a specific book issue by its unique identifier.
    /// Use this endpoint to get detailed information about a specific book lending transaction.
    /// </summary>
    /// <param name="issue_id">The unique identifier of the book issue record (must be greater than 0)</param>
    /// <returns>The BookIssue object if found, null otherwise</returns>
    /// <example>
    /// <code>
    /// // Get book issue with ID 1
    /// var issue = await GetAsync("1");
    /// </code>
    /// </example>
    [OperationContract]
    [WebGet(UriTemplate = "/bookissues/{issue_id}", ResponseFormat = WebMessageFormat.Json)]
    Task<BookIssue?> GetAsync(string issue_id);

    /// <summary>
    /// Creates a new book issue record in the system.
    /// Use this endpoint to record a new book lending transaction to a student.
    /// </summary>
    /// <param name="item">The book issue object to create. The issue_id field is ignored and will be assigned automatically.</param>
    /// <returns>The created BookIssue object with assigned ID</returns>
    /// <example>
    /// <code>
    /// // Create a new book issue
    /// var newIssue = new BookIssue 
    /// {
    ///     book_id = 1,
    ///     student_id = 101,
    ///     issue_date = DateTime.Now,
    ///     return_date = DateTime.Now.AddDays(14),
    ///     status = "Issued"
    /// };
    /// var createdIssue = await CreateAsync(newIssue);
    /// </code>
    /// </example>
    [OperationContract]
    [WebInvoke(Method = "POST", UriTemplate = "/bookissues", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
    Task<BookIssue> CreateAsync(BookIssue item);

    /// <summary>
    /// Updates an existing book issue record with new information.
    /// Use this endpoint to modify an existing book issue, such as updating return date or status.
    /// </summary>
    /// <param name="issue_id">The unique identifier of the book issue to update (must match the ID in the item parameter)</param>
    /// <param name="item">The book issue object with updated information</param>
    /// <returns>The updated BookIssue object if successful, null otherwise</returns>
    /// <example>
    /// <code>
    /// // Update book issue with ID 1
    /// var updatedIssue = new BookIssue 
    /// {
    ///     issue_id = 1,
    ///     book_id = 1,
    ///     student_id = 101,
    ///     issue_date = DateTime.Now,
    ///     return_date = DateTime.Now.AddDays(21),
    ///     status = "Extended"
    /// };
    /// var result = await UpdateAsync("1", updatedIssue);
    /// </code>
    /// </example>
    [OperationContract]
    [WebInvoke(Method = "PUT", UriTemplate = "/bookissues/{issue_id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
    Task<BookIssue?> UpdateAsync(string issue_id, BookIssue item);

    /// <summary>
    /// Removes a book issue record from the system by its unique identifier.
    /// Use this endpoint to delete a book issue record, typically used for data correction.
    /// </summary>
    /// <param name="issue_id">The unique identifier of the book issue to remove (must be greater than 0)</param>
    /// <returns>The removed BookIssue object if successful, null otherwise</returns>
    /// <example>
    /// <code>
    /// // Remove book issue with ID 1
    /// var removedIssue = await RemoveAsync("1");
    /// </code>
    /// </example>
    [OperationContract]
    [WebInvoke(Method = "DELETE", UriTemplate = "/bookissues/{issue_id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
    Task<BookIssue?> RemoveAsync(string issue_id);
}