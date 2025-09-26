namespace SIH.ERP.Soap.Models;

/// <summary>
/// Represents a book lending transaction in the educational institution's library.
/// This model stores information about book issues to students, including issue dates and return status.
/// </summary>
public class BookIssue
{
    /// <summary>
    /// Unique identifier for the book issue transaction.
    /// This is an auto-generated primary key assigned by the system.
    /// </summary>
    /// <example>1</example>
    public int issue_id { get; set; }
    
    /// <summary>
    /// Identifier of the library book being issued.
    /// References the Library table to identify which book is being lent.
    /// </summary>
    /// <example>1</example>
    public int? book_id { get; set; }
    
    /// <summary>
    /// Identifier of the student to whom the book is issued.
    /// References the Student table to identify which student borrowed the book.
    /// </summary>
    /// <example>101</example>
    public int? student_id { get; set; }
    
    /// <summary>
    /// Date when the book was issued to the student.
    /// Used to track lending period and calculate due dates.
    /// </summary>
    /// <example>2025-09-25T00:00:00</example>
    public DateTime? issue_date { get; set; }
    
    /// <summary>
    /// Date when the book is expected to be returned.
    /// Used to track due dates and identify overdue items.
    /// </summary>
    /// <example>2025-10-09T00:00:00</example>
    public DateTime? return_date { get; set; }
    
    /// <summary>
    /// Current status of the book issue (e.g., Issued, Returned, Overdue).
    /// Used to track the current state of the lending transaction.
    /// </summary>
    /// <example>Issued</example>
    public string? status { get; set; }
}