namespace SIH.ERP.Soap.Models;

/// <summary>
/// Represents a user-role assignment in the ERP system.
/// This model stores the mapping between users and their assigned roles for access control.
/// </summary>
public class UserRole
{
    /// <summary>
    /// Unique identifier for the user-role assignment record.
    /// This is an auto-generated primary key assigned by the system.
    /// </summary>
    /// <example>1</example>
    public int user_role_id { get; set; }
    
    /// <summary>
    /// Identifier of the user being assigned a role.
    /// References the User table to identify which user this assignment belongs to.
    /// </summary>
    /// <example>1</example>
    public int? user_id { get; set; }
    
    /// <summary>
    /// Identifier of the role being assigned to the user.
    /// References the Role table to identify which role is being assigned.
    /// </summary>
    /// <example>2</example>
    public int? role_id { get; set; }
}