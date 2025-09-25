namespace SIH.ERP.Soap.Models;

/// <summary>
/// Represents an examination result for a student in the educational institution.
/// This model stores information about student performance, including marks and grades.
/// </summary>
public class Result
{
    /// <summary>
    /// Unique identifier for the result record.
    /// This is an auto-generated primary key assigned by the system.
    /// </summary>
    /// <example>1</example>
    public int result_id { get; set; }
    
    /// <summary>
    /// Identifier of the examination this result belongs to.
    /// References the Exam table to identify which examination this result is for.
    /// </summary>
    /// <example>1</example>
    public int? exam_id { get; set; }
    
    /// <summary>
    /// Identifier of the student who took the examination.
    /// References the Student table to identify which student this result belongs to.
    /// </summary>
    /// <example>101</example>
    public int? student_id { get; set; }
    
    /// <summary>
    /// Marks obtained by the student in the examination.
    /// Used for academic performance evaluation and grading.
    /// </summary>
    /// <example>85</example>
    public int? marks { get; set; }
    
    /// <summary>
    /// Grade assigned to the student based on their marks.
    /// Used for academic performance reporting and transcript generation.
    /// </summary>
    /// <example>A</example>
    public string? grade { get; set; }
}