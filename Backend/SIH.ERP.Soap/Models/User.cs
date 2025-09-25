namespace SIH.ERP.Soap.Models;

/// <summary>
/// Represents a user in the ERP system.
/// This model stores user account information, authentication details, and account status.
/// </summary>
public class User
{
    /// <summary>
    /// Unique identifier for the user account.
    /// This is an auto-generated primary key assigned by the system.
    /// </summary>
    /// <example>1</example>
    public int user_id { get; set; }
    
    /// <summary>
    /// Full name of the user.
    /// Used for identification and personalization within the system.
    /// </summary>
    /// <example>John Doe</example>
    public string full_name { get; set; } = string.Empty;
    
    /// <summary>
    /// Email address of the user.
    /// Used for account identification, communication, and password recovery.
    /// </summary>
    /// <example>john.doe@example.com</example>
    public string email { get; set; } = string.Empty;
    
    /// <summary>
    /// Date of birth of the user.
    /// Used for age verification and personalization purposes.
    /// </summary>
    /// <example>1990-01-01T00:00:00</example>
    public DateTime dob { get; set; }
    
    /// <summary>
    /// Hashed password for the user account.
    /// Used for authentication and account security.
    /// </summary>
    /// <example>hashed_password_value</example>
    public string password_hash { get; set; } = string.Empty;
    
    /// <summary>
    /// Indicates whether the user account is active and can be used.
    /// Used for account management and security control.
    /// </summary>
    /// <example>true</example>
    public bool is_active { get; set; }
    
    /// <summary>
    /// Date and time when the user account was created.
    /// System-generated timestamp for audit and tracking purposes.
    /// </summary>
    /// <example>2025-09-25T09:00:00</example>
    public DateTime created_at { get; set; }
    
    /// <summary>
    /// Date and time when the user account was last updated.
    /// System-generated timestamp for audit and tracking purposes.
    /// </summary>
    /// <example>2025-09-25T14:45:00</example>
    public DateTime updated_at { get; set; }
}