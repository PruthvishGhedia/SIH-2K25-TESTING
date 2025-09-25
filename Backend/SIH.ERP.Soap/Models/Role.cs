namespace SIH.ERP.Soap.Models;

/// <summary>
/// Represents a role in the ERP system
/// </summary>
public class Role
{
    /// <summary>
    /// Unique identifier for the role
    /// </summary>
    public int role_id { get; set; }
    
    /// <summary>
    /// Name of the role
    /// </summary>
    public string role_name { get; set; } = string.Empty;
    
    /// <summary>
    /// Description of the role
    /// </summary>
    public string? role_description { get; set; }
}