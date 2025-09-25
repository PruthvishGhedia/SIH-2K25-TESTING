namespace SIH.ERP.Soap.Models;

/// <summary>
/// Represents a user in the ERP system
/// </summary>
public class User
{
    /// <summary>
    /// Unique identifier for the user
    /// </summary>
    public int user_id { get; set; }
    
    /// <summary>
    /// User's full name
    /// </summary>
    public string full_name { get; set; } = string.Empty;
    
    /// <summary>
    /// User's email address
    /// </summary>
    public string email { get; set; } = string.Empty;
    
    /// <summary>
    /// User's date of birth
    /// </summary>
    public DateTime dob { get; set; }
    
    /// <summary>
    /// Hashed password for the user
    /// </summary>
    public string password_hash { get; set; } = string.Empty;
    
    /// <summary>
    /// Indicates if the user account is active
    /// </summary>
    public bool is_active { get; set; }
    
    /// <summary>
    /// Date and time when the user account was created
    /// </summary>
    public DateTime created_at { get; set; }
    
    /// <summary>
    /// Date and time when the user account was last updated
    /// </summary>
    public DateTime updated_at { get; set; }
}