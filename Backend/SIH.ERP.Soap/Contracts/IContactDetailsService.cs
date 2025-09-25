using CoreWCF;
using CoreWCF.Web;
using SIH.ERP.Soap.Models;

namespace SIH.ERP.Soap.Contracts;

/// <summary>
/// Service contract for managing contact details in the ERP system.
/// This service handles various contact information for users, including phone numbers, emails, and addresses.
/// </summary>
[ServiceContract]
public interface IContactDetailsService
{
    /// <summary>
    /// Retrieves a list of contact details with pagination support.
    /// Use this endpoint to get multiple contact records with optional pagination.
    /// </summary>
    /// <param name="limit">Maximum number of contact details to retrieve (default: 100, maximum: 1000)</param>
    /// <param name="offset">Number of contact details to skip for pagination (default: 0)</param>
    /// <returns>A collection of ContactDetails objects</returns>
    /// <example>
    /// <code>
    /// // Get first 10 contact details
    /// var contacts = await ListAsync(10, 0);
    /// </code>
    /// </example>
    [OperationContract]
    [WebGet(UriTemplate = "/contactdetails?limit={limit}&offset={offset}", ResponseFormat = WebMessageFormat.Json)]
    Task<IEnumerable<ContactDetails>> ListAsync(int limit = 100, int offset = 0);

    /// <summary>
    /// Retrieves a specific contact detail record by its unique identifier.
    /// Use this endpoint to get detailed information about a specific user's contact method.
    /// </summary>
    /// <param name="contact_id">The unique identifier of the contact detail record (must be greater than 0)</param>
    /// <returns>The ContactDetails object if found, null otherwise</returns>
    /// <example>
    /// <code>
    /// // Get contact detail with ID 1
    /// var contact = await GetAsync("1");
    /// </code>
    /// </example>
    [OperationContract]
    [WebGet(UriTemplate = "/contactdetails/{contact_id}", ResponseFormat = WebMessageFormat.Json)]
    Task<ContactDetails?> GetAsync(string contact_id);

    /// <summary>
    /// Creates a new contact detail record in the system.
    /// Use this endpoint to add a new contact method for a user.
    /// </summary>
    /// <param name="item">The contact details object to create. The contact_id field is ignored and will be assigned automatically.</param>
    /// <returns>The created ContactDetails object with assigned ID</returns>
    /// <example>
    /// <code>
    /// // Create a new contact detail
    /// var newContact = new ContactDetails 
    /// {
    ///     user_id = 1,
    ///     contact_type = "Email",
    ///     contact_value = "john.doe@example.com",
    ///     is_primary = true
    /// };
    /// var createdContact = await CreateAsync(newContact);
    /// </code>
    /// </example>
    [OperationContract]
    [WebInvoke(Method = "POST", UriTemplate = "/contactdetails", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
    Task<ContactDetails> CreateAsync(ContactDetails item);

    /// <summary>
    /// Updates an existing contact detail record with new information.
    /// Use this endpoint to modify an existing contact record, such as updating a phone number or email.
    /// </summary>
    /// <param name="contact_id">The unique identifier of the contact detail to update (must match the ID in the item parameter)</param>
    /// <param name="item">The contact details object with updated information</param>
    /// <returns>The updated ContactDetails object if successful, null otherwise</returns>
    /// <example>
    /// <code>
    /// // Update contact detail with ID 1
    /// var updatedContact = new ContactDetails 
    /// {
    ///     contact_id = 1,
    ///     user_id = 1,
    ///     contact_type = "Email",
    ///     contact_value = "john.doe.updated@example.com",
    ///     is_primary = true
    /// };
    /// var result = await UpdateAsync("1", updatedContact);
    /// </code>
    /// </example>
    [OperationContract]
    [WebInvoke(Method = "PUT", UriTemplate = "/contactdetails/{contact_id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
    Task<ContactDetails?> UpdateAsync(string contact_id, ContactDetails item);

    /// <summary>
    /// Removes a contact detail record from the system by its unique identifier.
    /// Use this endpoint to delete a contact record, typically used when contact methods are no longer valid.
    /// </summary>
    /// <param name="contact_id">The unique identifier of the contact detail to remove (must be greater than 0)</param>
    /// <returns>The removed ContactDetails object if successful, null otherwise</returns>
    /// <example>
    /// <code>
    /// // Remove contact detail with ID 1
    /// var removedContact = await RemoveAsync("1");
    /// </code>
    /// </example>
    [OperationContract]
    [WebInvoke(Method = "DELETE", UriTemplate = "/contactdetails/{contact_id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
    Task<ContactDetails?> RemoveAsync(string contact_id);
}