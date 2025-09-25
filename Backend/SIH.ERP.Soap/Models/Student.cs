namespace SIH.ERP.Soap.Models;

/// <summary>
/// Represents a student in the educational institution.
/// This model stores comprehensive information about students, including personal details, academic information, and enrollment data.
/// </summary>
public class Student
{
    /// <summary>
    /// Unique identifier for the student record.
    /// This is an auto-generated primary key assigned by the system.
    /// </summary>
    /// <example>1</example>
    public int student_id { get; set; }
    
    /// <summary>
    /// First name of the student.
    /// Used for identification and communication purposes.
    /// </summary>
    /// <example>John</example>
    public string first_name { get; set; } = string.Empty;
    
    /// <summary>
    /// Last name (surname) of the student.
    /// Used for identification and communication purposes.
    /// </summary>
    /// <example>Doe</example>
    public string last_name { get; set; } = string.Empty;
    
    /// <summary>
    /// Date of birth of the student.
    /// Used for age verification, academic planning, and identification purposes.
    /// </summary>
    /// <example>2000-01-01T00:00:00</example>
    public DateTime dob { get; set; }
    
    /// <summary>
    /// Email address of the student.
    /// Used for communication, account access, and official correspondence.
    /// </summary>
    /// <example>john.doe@example.com</example>
    public string email { get; set; } = string.Empty;
    
    /// <summary>
    /// Identifier of the department the student belongs to.
    /// References the Department table to identify the student's academic department.
    /// </summary>
    /// <example>1</example>
    public int? department_id { get; set; }
    
    /// <summary>
    /// Identifier of the course the student is enrolled in.
    /// References the Course table to identify the student's academic program.
    /// </summary>
    /// <example>101</example>
    public int? course_id { get; set; }
    
    /// <summary>
    /// Identifier of the student's guardian or parent.
    /// References the Guardian table to identify the student's emergency contact and family information.
    /// </summary>
    /// <example>201</example>
    public int? guardian_id { get; set; }
    
    /// <summary>
    /// Date when the student was admitted to the institution.
    /// Used for academic planning, cohort analysis, and administrative tracking.
    /// </summary>
    /// <example>2025-09-01</example>
    public string? admission_date { get; set; }
    
    /// <summary>
    /// Indicates whether the student's information has been verified.
    /// Used to track the validation status of student records for compliance and accuracy.
    /// </summary>
    /// <example>true</example>
    public bool? verified { get; set; }
}