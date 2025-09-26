namespace SIH.ERP.Soap.Models;

/// <summary>
/// Represents a subject in the educational institution.
/// This model stores information about academic subjects, including their association with courses and credit hours.
/// </summary>
public class Subject
{
    /// <summary>
    /// Unique identifier for the subject record.
    /// This is an auto-generated primary key assigned by the system.
    /// </summary>
    /// <example>1</example>
    public int subject_code { get; set; }
    
    /// <summary>
    /// Identifier of the course this subject belongs to.
    /// References the Course table to identify the academic program this subject is part of.
    /// </summary>
    /// <example>101</example>
    public int? course_id { get; set; }
    
    /// <summary>
    /// Name of the subject.
    /// Used for identification, scheduling, and academic planning purposes.
    /// </summary>
    /// <example>Data Structures</example>
    public string subject_name { get; set; } = string.Empty;
    
    /// <summary>
    /// Number of credit hours assigned to this subject.
    /// Used for academic planning, GPA calculation, and graduation requirements tracking.
    /// </summary>
    /// <example>3</example>
    public int? credits { get; set; }
}