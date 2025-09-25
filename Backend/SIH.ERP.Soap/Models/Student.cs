namespace SIH.ERP.Soap.Models;

/// <summary>
/// Represents a student in the educational institution
/// </summary>
public class Student
{
    /// <summary>
    /// Unique identifier for the student
    /// </summary>
    public int student_id { get; set; }
    
    /// <summary>
    /// Student's first name
    /// </summary>
    public string first_name { get; set; } = string.Empty;
    
    /// <summary>
    /// Student's last name
    /// </summary>
    public string last_name { get; set; } = string.Empty;
    
    /// <summary>
    /// Student's date of birth
    /// </summary>
    public DateTime dob { get; set; }
    
    /// <summary>
    /// Student's email address
    /// </summary>
    public string email { get; set; } = string.Empty;
    
    /// <summary>
    /// Identifier of the department the student belongs to
    /// </summary>
    public int? department_id { get; set; }
    
    /// <summary>
    /// Identifier of the course the student is enrolled in
    /// </summary>
    public int? course_id { get; set; }
    
    /// <summary>
    /// Identifier of the student's guardian
    /// </summary>
    public int? guardian_id { get; set; }
    
    /// <summary>
    /// Date of admission
    /// </summary>
    public string? admission_date { get; set; }
    
    /// <summary>
    /// Indicates if the student's information has been verified
    /// </summary>
    public bool? verified { get; set; }
}