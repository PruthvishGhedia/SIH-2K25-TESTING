namespace SIH.ERP.Soap.Models;

/// <summary>
/// Represents contact information for users in the ERP system.
/// This model stores various contact methods for users, including phone numbers, emails, and addresses.
/// </summary>
public class ContactDetails
{
    /// <summary>
    /// Unique identifier for the contact detail record.
    /// This is an auto-generated primary key assigned by the system.
    /// </summary>
    /// <example>1</example>
    public int contact_id { get; set; }
    
    /// <summary>
    /// Identifier of the user this contact information belongs to.
    /// References the User table to identify which user this contact detail belongs to.
    /// </summary>
    /// <example>1</example>
    public int? user_id { get; set; }
    
    /// <summary>
    /// Type of contact information (e.g., Email, Phone, Address, Mobile).
    /// Used to categorize and identify different contact methods.
    /// </summary>
    /// <example>Email</example>
    public string? contact_type { get; set; }
    
    /// <summary>
    /// Actual contact information value (e.g., email address, phone number, street address).
    /// Used for communication and identification purposes.
    /// </summary>
    /// <example>john.doe@example.com</example>
    public string? contact_value { get; set; }
    
    /// <summary>
    /// Indicates if this is the primary contact method of its type for the user.
    /// Used to determine which contact method should be used as the default for communication.
    /// </summary>
    /// <example>true</example>
    public bool? is_primary { get; set; }
}