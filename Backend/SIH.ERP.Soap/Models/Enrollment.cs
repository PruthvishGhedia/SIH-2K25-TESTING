namespace SIH.ERP.Soap.Models;

/// <summary>
/// Represents a student enrollment in a course in the educational institution.
/// This model stores information about student course enrollments, including enrollment dates and status.
/// </summary>
public class Enrollment
{
    /// <summary>
    /// Unique identifier for the enrollment record.
    /// This is an auto-generated primary key assigned by the system.
    /// </summary>
    /// <example>1</example>
    public int enrollment_id { get; set; }
    
    /// <summary>
    /// Identifier of the student being enrolled.
    /// References the Student table to identify which student this enrollment belongs to.
    /// </summary>
    /// <example>1</example>
    public int student_id { get; set; }
    
    /// <summary>
    /// Identifier of the course the student is being enrolled in.
    /// References the Course table to identify which course this enrollment is for.
    /// </summary>
    /// <example>101</example>
    public int course_id { get; set; }
    
    /// <summary>
    /// Date when the student was enrolled in the course.
    /// Used for academic planning and tracking enrollment timelines.
    /// </summary>
    /// <example>2025-09-01</example>
    public string enrollment_date { get; set; } = string.Empty;
    
    /// <summary>
    /// Current status of the enrollment (e.g., Active, Completed, Withdrawn).
    /// Used to track the current state of the student's course enrollment.
    /// </summary>
    /// <example>Active</example>
    public string? status { get; set; }
}