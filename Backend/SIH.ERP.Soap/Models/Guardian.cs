namespace SIH.ERP.Soap.Models;

/// <summary>
/// Represents a guardian or parent of a student in the educational institution.
/// This model stores contact information and relationship details for student guardians.
/// </summary>
public class Guardian
{
    /// <summary>
    /// Unique identifier for the guardian record.
    /// This is an auto-generated primary key assigned by the system.
    /// </summary>
    /// <example>1</example>
    public int guardian_id { get; set; }
    
    /// <summary>
    /// Identifier of the student this guardian is associated with.
    /// References the Student table to identify which student this guardian information belongs to.
    /// </summary>
    /// <example>1</example>
    public int? student_id { get; set; }
    
    /// <summary>
    /// Name of the guardian or parent.
    /// Used for identification and communication purposes.
    /// </summary>
    /// <example>Jane Doe</example>
    public string? name { get; set; }
    
    /// <summary>
    /// Relationship of the guardian to the student (e.g., Mother, Father, Legal Guardian).
    /// Used for emergency contact and administrative purposes.
    /// </summary>
    /// <example>Mother</example>
    public string? relationship { get; set; }
    
    /// <summary>
    /// Mobile phone number of the guardian.
    /// Used for emergency contact and communication purposes.
    /// </summary>
    /// <example>+1234567890</example>
    public string? mobile { get; set; }
    
    /// <summary>
    /// Address of the guardian.
    /// Used for communication, mailing, and emergency purposes.
    /// </summary>
    /// <example>123 Main St, City, State, ZIP</example>
    public string? address { get; set; }
}