namespace SIH.ERP.Soap.Models;

/// <summary>
/// Represents a student attendance record in the educational institution.
/// This model stores information about student presence in courses on specific dates.
/// </summary>
public class Attendance
{
    /// <summary>
    /// Unique identifier for the attendance record.
    /// This is an auto-generated primary key assigned by the system.
    /// </summary>
    /// <example>1</example>
    public int attendance_id { get; set; }
    
    /// <summary>
    /// Identifier of the student whose attendance is being recorded.
    /// References the Student table to identify which student this attendance record belongs to.
    /// </summary>
    /// <example>1</example>
    public int student_id { get; set; }
    
    /// <summary>
    /// Identifier of the course for which attendance is being recorded.
    /// References the Course table to identify which course this attendance is for.
    /// </summary>
    /// <example>101</example>
    public int course_id { get; set; }
    
    /// <summary>
    /// Date of the attendance record.
    /// Used to track when the attendance was recorded.
    /// </summary>
    /// <example>2025-09-25</example>
    public string date { get; set; } = string.Empty;
    
    /// <summary>
    /// Indicates whether the student was present (true) or absent (false) on the specified date.
    /// Used for attendance tracking and academic performance monitoring.
    /// </summary>
    /// <example>true</example>
    public bool present { get; set; }
}