using CoreWCF;
using CoreWCF.Web;
using SIH.ERP.Soap.Models;

namespace SIH.ERP.Soap.Contracts;

/// <summary>
/// Service contract for managing library items in the ERP system.
/// This service handles library books and resources, including creation, modification, and removal of library records.
/// </summary>
[ServiceContract]
public interface ILibraryService
{
    /// <summary>
    /// Retrieves a list of library items with pagination support.
    /// Use this endpoint to get multiple library records with optional pagination.
    /// </summary>
    /// <param name="limit">Maximum number of library items to retrieve (default: 100, maximum: 1000)</param>
    /// <param name="offset">Number of library items to skip for pagination (default: 0)</param>
    /// <returns>A collection of Library objects</returns>
    /// <example>
    /// <code>
    /// // Get first 10 library items
    /// var books = await ListAsync(10, 0);
    /// </code>
    /// </example>
    [OperationContract]
    [WebGet(UriTemplate = "/library?limit={limit}&offset={offset}", ResponseFormat = WebMessageFormat.Json)]
    Task<IEnumerable<Library>> ListAsync(int limit = 100, int offset = 0);

    /// <summary>
    /// Retrieves a specific library item by its unique identifier.
    /// Use this endpoint to get detailed information about a specific library resource.
    /// </summary>
    /// <param name="book_id">The unique identifier of the library item (must be greater than 0)</param>
    /// <returns>The Library object if found, null otherwise</returns>
    /// <example>
    /// <code>
    /// // Get library item with ID 1
    /// var book = await GetAsync("1");
    /// </code>
    /// </example>
    [OperationContract]
    [WebGet(UriTemplate = "/library/{book_id}", ResponseFormat = WebMessageFormat.Json)]
    Task<Library?> GetAsync(string book_id);

    /// <summary>
    /// Creates a new library item record in the system.
    /// Use this endpoint to add a new book or resource to the library collection.
    /// </summary>
    /// <param name="item">The library item object to create. The book_id field is ignored and will be assigned automatically.</param>
    /// <returns>The created Library object with assigned ID</returns>
    /// <example>
    /// <code>
    /// // Create a new library item
    /// var newBook = new Library 
    /// {
    ///     title = "Introduction to Computer Science",
    ///     author = "John Doe",
    ///     shelf = "CS-A1",
    ///     isbn = "978-1234567890",
    ///     copies = 5
    /// };
    /// var createdBook = await CreateAsync(newBook);
    /// </code>
    /// </example>
    [OperationContract]
    [WebInvoke(Method = "POST", UriTemplate = "/library", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
    Task<Library> CreateAsync(Library item);

    /// <summary>
    /// Updates an existing library item record with new information.
    /// Use this endpoint to modify an existing library record, such as updating copies count or shelf location.
    /// </summary>
    /// <param name="book_id">The unique identifier of the library item to update (must match the ID in the item parameter)</param>
    /// <param name="item">The library item object with updated information</param>
    /// <returns>The updated Library object if successful, null otherwise</returns>
    /// <example>
    /// <code>
    /// // Update library item with ID 1
    /// var updatedBook = new Library 
    /// {
    ///     book_id = 1,
    ///     title = "Introduction to Computer Science",
    ///     author = "John Doe",
    ///     shelf = "CS-B2",
    ///     isbn = "978-1234567890",
    ///     copies = 3
    /// };
    /// var result = await UpdateAsync("1", updatedBook);
    /// </code>
    /// </example>
    [OperationContract]
    [WebInvoke(Method = "PUT", UriTemplate = "/library/{book_id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
    Task<Library?> UpdateAsync(string book_id, Library item);

    /// <summary>
    /// Removes a library item record from the system by its unique identifier.
    /// Use this endpoint to delete a library record, typically used when items are lost or removed from collection.
    /// </summary>
    /// <param name="book_id">The unique identifier of the library item to remove (must be greater than 0)</param>
    /// <returns>The removed Library object if successful, null otherwise</returns>
    /// <example>
    /// <code>
    /// // Remove library item with ID 1
    /// var removedBook = await RemoveAsync("1");
    /// </code>
    /// </example>
    [OperationContract]
    [WebInvoke(Method = "DELETE", UriTemplate = "/library/{book_id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
    Task<Library?> RemoveAsync(string book_id);
}