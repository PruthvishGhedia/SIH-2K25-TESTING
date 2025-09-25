namespace SIH.ERP.Soap.Models;

/// <summary>
/// Represents a hostel room allocation for a student in the educational institution.
/// This model stores information about student accommodation assignments, including allocation periods and status.
/// </summary>
public class HostelAllocation
{
    /// <summary>
    /// Unique identifier for the allocation record.
    /// This is an auto-generated primary key assigned by the system.
    /// </summary>
    /// <example>1</example>
    public int allocation_id { get; set; }
    
    /// <summary>
    /// Identifier of the student assigned to the hostel room.
    /// References the Student table to identify which student this allocation belongs to.
    /// </summary>
    /// <example>1</example>
    public int? student_id { get; set; }
    
    /// <summary>
    /// Identifier of the hostel facility for this allocation.
    /// References the Hostel table to identify which hostel this allocation is for.
    /// </summary>
    /// <example>1</example>
    public int? hostel_id { get; set; }
    
    /// <summary>
    /// Identifier of the specific room assigned to the student.
    /// References the Room table to identify which room this allocation is for.
    /// </summary>
    /// <example>101</example>
    public int? room_id { get; set; }
    
    /// <summary>
    /// Start date of the hostel allocation period.
    /// Used to track when the student's accommodation begins.
    /// </summary>
    /// <example>2025-09-01T00:00:00</example>
    public DateTime? start_date { get; set; }
    
    /// <summary>
    /// End date of the hostel allocation period.
    /// Used to track when the student's accommodation ends.
    /// </summary>
    /// <example>2026-08-31T00:00:00</example>
    public DateTime? end_date { get; set; }
    
    /// <summary>
    /// Current status of the allocation (e.g., Active, Expired, Cancelled).
    /// Used to track the current state of the student's accommodation.
    /// </summary>
    /// <example>Active</example>
    public string? status { get; set; }
    
    /// <summary>
    /// Date and time when the allocation record was created.
    /// System-generated timestamp for audit and tracking purposes.
    /// </summary>
    /// <example>2025-09-25T09:00:00</example>
    public DateTime? created_at { get; set; }
    
    /// <summary>
    /// Date and time when the allocation record was last updated.
    /// System-generated timestamp for audit and tracking purposes.
    /// </summary>
    /// <example>2025-09-25T14:45:00</example>
    public DateTime? updated_at { get; set; }
}