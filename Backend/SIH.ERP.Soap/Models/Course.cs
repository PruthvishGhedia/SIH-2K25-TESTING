namespace SIH.ERP.Soap.Models;

/// <summary>
/// Represents a course offered by the educational institution.
/// This model stores information about academic programs, including curriculum details, department association, and financial information.
/// </summary>
public class Course
{
    /// <summary>
    /// Unique identifier for the course record.
    /// This is an auto-generated primary key assigned by the system.
    /// </summary>
    /// <example>1</example>
    public int course_id { get; set; }
    
    /// <summary>
    /// Name of the course or academic program.
    /// Used for identification, marketing, and academic planning purposes.
    /// </summary>
    /// <example>Computer Science</example>
    public string course_name { get; set; } = string.Empty;
    
    /// <summary>
    /// Code assigned to the course for identification and cataloging.
    /// Used in scheduling, enrollment, and academic record systems.
    /// </summary>
    /// <example>CS101</example>
    public string course_code { get; set; } = string.Empty;
    
    /// <summary>
    /// Detailed description of the course content and objectives.
    /// Used for student information, academic advising, and marketing materials.
    /// </summary>
    /// <example>Introduction to Computer Science covers programming fundamentals, algorithms, and data structures.</example>
    public string? course_description { get; set; }
    
    /// <summary>
    /// Identifier of the department offering the course.
    /// References the Department table to identify the academic department responsible for the course.
    /// </summary>
    /// <example>1</example>
    public int? dept_id { get; set; }
    
    /// <summary>
    /// Duration of the course in years or semesters.
    /// Used for academic planning, scheduling, and graduation requirements tracking.
    /// </summary>
    /// <example>4</example>
    public int? duration { get; set; }
    
    /// <summary>
    /// Fees associated with the course for the academic period.
    /// Used for financial planning, billing, and student payment processing.
    /// </summary>
    /// <example>5000.00</example>
    public decimal? fees { get; set; }
}