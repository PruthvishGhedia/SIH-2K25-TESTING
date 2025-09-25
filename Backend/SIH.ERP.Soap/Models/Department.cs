namespace SIH.ERP.Soap.Models;

/// <summary>
/// Represents a department in the educational institution.
/// This model stores information about academic and administrative departments, including leadership and descriptive information.
/// </summary>
public class Department
{
    /// <summary>
    /// Unique identifier for the department record.
    /// This is an auto-generated primary key assigned by the system.
    /// </summary>
    /// <example>1</example>
    public int dept_id { get; set; }
    
    /// <summary>
    /// Name of the department.
    /// Used for identification, academic planning, and administrative purposes.
    /// </summary>
    /// <example>Computer Science</example>
    public string dept_name { get; set; } = string.Empty;
    
    /// <summary>
    /// Description of the department.
    /// Provides additional information about the department's focus, mission, or offerings.
    /// </summary>
    /// <example>Department of Computer Science and Information Technology</example>
    public string? dept_description { get; set; }
    
    /// <summary>
    /// Identifier of the head of the department (faculty member).
    /// References the faculty member who leads this department.
    /// </summary>
    /// <example>101</example>
    public int? hod_id { get; set; }
}