namespace SIH.ERP.Soap.Models;

/// <summary>
/// Represents an examination in the educational institution.
/// This model stores information about exams, including scheduling, assessment type, and maximum marks.
/// </summary>
public class Exam
{
    /// <summary>
    /// Unique identifier for the exam record.
    /// This is an auto-generated primary key assigned by the system.
    /// </summary>
    /// <example>1</example>
    public int exam_id { get; set; }
    
    /// <summary>
    /// Identifier of the department conducting the exam.
    /// References the Department table to identify which academic department is responsible.
    /// </summary>
    /// <example>1</example>
    public int? dept_id { get; set; }
    
    /// <summary>
    /// Identifier of the subject being examined.
    /// References the Subject table to identify which subject this exam covers.
    /// </summary>
    /// <example>101</example>
    public int? subject_code { get; set; }
    
    /// <summary>
    /// Date and time when the exam is scheduled to be conducted.
    /// Used for academic planning and student preparation.
    /// </summary>
    /// <example>2025-12-15T00:00:00</example>
    public DateTime? exam_date { get; set; }
    
    /// <summary>
    /// Type of assessment (e.g., Midterm, Final, Quiz, Practical).
    /// Used to categorize exams for academic planning and grading.
    /// </summary>
    /// <example>Final</example>
    public string? assessment_type { get; set; }
    
    /// <summary>
    /// Maximum marks that can be achieved in this exam.
    /// Used for grading and academic performance evaluation.
    /// </summary>
    /// <example>100</example>
    public int? max_marks { get; set; }
    
    /// <summary>
    /// Identifier of the faculty member who created the exam record.
    /// References the User table to identify which staff member scheduled the exam.
    /// </summary>
    /// <example>201</example>
    public int? created_by { get; set; }
    
    /// <summary>
    /// Date and time when the exam record was created.
    /// System-generated timestamp for audit and tracking purposes.
    /// </summary>
    /// <example>2025-09-25T09:00:00</example>
    public DateTime? created_at { get; set; }
    
    /// <summary>
    /// Date and time when the exam record was last updated.
    /// System-generated timestamp for audit and tracking purposes.
    /// </summary>
    /// <example>2025-09-25T14:45:00</example>
    public DateTime? updated_at { get; set; }
}