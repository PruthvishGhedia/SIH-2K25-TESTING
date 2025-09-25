namespace SIH.ERP.Soap.Models;

/// <summary>
/// Represents a role in the ERP system.
/// This model defines user roles and permissions, controlling access to various system features and capabilities.
/// </summary>
public class Role
{
    /// <summary>
    /// Unique identifier for the role record.
    /// This is an auto-generated primary key assigned by the system.
    /// </summary>
    /// <example>1</example>
    public int role_id { get; set; }
    
    /// <summary>
    /// Name of the role.
    /// Used for identification and assignment to users.
    /// </summary>
    /// <example>Administrator</example>
    public string role_name { get; set; } = string.Empty;
    
    /// <summary>
    /// Description of the role and its permissions.
    /// Provides information about the capabilities and access level associated with this role.
    /// </summary>
    /// <example>System administrator with full access to all features</example>
    public string? role_description { get; set; }
}