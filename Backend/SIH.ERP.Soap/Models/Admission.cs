namespace SIH.ERP.Soap.Models;

/// <summary>
/// Represents a student admission application in the educational institution.
/// This model stores all information related to a student's admission process.
/// </summary>
public class Admission
{
    /// <summary>
    /// Unique identifier for the admission record.
    /// This is an auto-generated primary key assigned by the system.
    /// </summary>
    /// <example>1</example>
    public int admission_id { get; set; }
    
    /// <summary>
    /// Full name of the prospective student.
    /// </summary>
    /// <example>John Doe</example>
    public string? full_name { get; set; }
    
    /// <summary>
    /// Email address of the prospective student.
    /// Used for communication throughout the admission process.
    /// </summary>
    /// <example>john.doe@example.com</example>
    public string? email { get; set; }
    
    /// <summary>
    /// Date of birth of the prospective student.
    /// Used for age verification and record matching.
    /// </summary>
    /// <example>2000-01-01T00:00:00</example>
    public DateTime? dob { get; set; }
    
    /// <summary>
    /// Contact number of the prospective student.
    /// Used for direct communication and verification.
    /// </summary>
    /// <example>+1234567890</example>
    public string? contact_no { get; set; }
    
    /// <summary>
    /// Residential address of the prospective student.
    /// Used for verification and communication purposes.
    /// </summary>
    /// <example>123 Main St, City, State, 12345</example>
    public string? address { get; set; }
    
    /// <summary>
    /// Identifier of the department the student is applying to.
    /// References the Department table to identify the academic department.
    /// </summary>
    /// <example>1</example>
    public int? dept_id { get; set; }
    
    /// <summary>
    /// Identifier of the course the student is applying for.
    /// References the Course table to identify the specific course.
    /// </summary>
    /// <example>1</example>
    public int? course_id { get; set; }
    
    /// <summary>
    /// Date when the admission application was submitted.
    /// Used for tracking application timelines and processing.
    /// </summary>
    /// <example>2025-09-25T00:00:00</example>
    public DateTime? applied_on { get; set; }
    
    /// <summary>
    /// Indicates if the admission application has been verified.
    /// Used by admission staff to mark applications as verified after document checking.
    /// </summary>
    /// <example>true</example>
    public bool? verified { get; set; }
    
    /// <summary>
    /// Indicates if the admission has been confirmed and accepted.
    /// Used to mark applications that have been approved for enrollment.
    /// </summary>
    /// <example>false</example>
    public bool? confirmed { get; set; }
    
    /// <summary>
    /// Date and time when the admission record was created.
    /// System-generated timestamp for audit and tracking purposes.
    /// </summary>
    /// <example>2025-09-25T10:30:00</example>
    public DateTime? created_at { get; set; }
    
    /// <summary>
    /// Date and time when the admission record was last updated.
    /// System-generated timestamp for audit and tracking purposes.
    /// </summary>
    /// <example>2025-09-25T14:45:00</example>
    public DateTime? updated_at { get; set; }
}