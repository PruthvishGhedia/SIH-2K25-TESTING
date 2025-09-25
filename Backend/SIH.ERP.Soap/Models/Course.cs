namespace SIH.ERP.Soap.Models;

/// <summary>
/// Represents a course offered by the educational institution
/// </summary>
public class Course
{
    /// <summary>
    /// Unique identifier for the course
    /// </summary>
    public int course_id { get; set; }
    
    /// <summary>
    /// Name of the course
    /// </summary>
    public string course_name { get; set; } = string.Empty;
    
    /// <summary>
    /// Code of the course
    /// </summary>
    public string course_code { get; set; } = string.Empty;
    
    /// <summary>
    /// Description of the course
    /// </summary>
    public string? course_description { get; set; }
    
    /// <summary>
    /// Identifier of the department offering the course
    /// </summary>
    public int? dept_id { get; set; }
    
    /// <summary>
    /// Duration of the course in years
    /// </summary>
    public int? duration { get; set; }
    
    /// <summary>
    /// Fees for the course
    /// </summary>
    public decimal? fees { get; set; }
}