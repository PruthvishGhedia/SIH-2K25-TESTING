namespace SIH.ERP.Soap.Models;

/// <summary>
/// Represents a faculty member in the educational institution.
/// This model stores information about faculty members, including personal details and employment information.
/// </summary>
public class Faculty
{
    /// <summary>
    /// Unique identifier for the faculty member record.
    /// This is an auto-generated primary key assigned by the system.
    /// </summary>
    /// <example>1</example>
    public int faculty_id { get; set; }
    
    /// <summary>
    /// First name of the faculty member.
    /// Used for identification and communication purposes.
    /// </summary>
    /// <example>John</example>
    public string first_name { get; set; } = string.Empty;
    
    /// <summary>
    /// Last name (surname) of the faculty member.
    /// Used for identification and communication purposes.
    /// </summary>
    /// <example>Smith</example>
    public string last_name { get; set; } = string.Empty;
    
    /// <summary>
    /// Email address of the faculty member.
    /// Used for institutional communication and account access.
    /// </summary>
    /// <example>john.smith@institution.edu</example>
    public string email { get; set; } = string.Empty;
    
    /// <summary>
    /// Phone number of the faculty member.
    /// Used for direct communication and emergency contact.
    /// </summary>
    /// <example>+1234567890</example>
    public string? phone { get; set; }
    
    /// <summary>
    /// Identifier of the department this faculty member belongs to.
    /// References the Department table to identify the academic department.
    /// </summary>
    /// <example>1</example>
    public int? department_id { get; set; }
    
    /// <summary>
    /// Indicates whether the faculty member is currently active.
    /// Used to track employment status and access permissions.
    /// </summary>
    /// <example>true</example>
    public bool? is_active { get; set; }
}