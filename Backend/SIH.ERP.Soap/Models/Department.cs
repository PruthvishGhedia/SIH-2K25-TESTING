namespace SIH.ERP.Soap.Models;

/// <summary>
/// Represents a department in the educational institution
/// </summary>
public class Department
{
    /// <summary>
    /// Unique identifier for the department
    /// </summary>
    public int dept_id { get; set; }
    
    /// <summary>
    /// Name of the department
    /// </summary>
    public string dept_name { get; set; } = string.Empty;
    
    /// <summary>
    /// Description of the department
    /// </summary>
    public string? dept_description { get; set; }
    
    /// <summary>
    /// Identifier of the head of the department
    /// </summary>
    public int? hod_id { get; set; }
}