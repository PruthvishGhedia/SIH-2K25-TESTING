namespace SIH.ERP.Soap.Models;

/// <summary>
/// Represents a hostel facility in the educational institution.
/// This model stores information about hostel facilities available for student accommodation.
/// </summary>
public class Hostel
{
    /// <summary>
    /// Unique identifier for the hostel facility.
    /// This is an auto-generated primary key assigned by the system.
    /// </summary>
    /// <example>1</example>
    public int hostel_id { get; set; }
    
    /// <summary>
    /// Name of the hostel facility.
    /// Used to identify and reference the hostel in reports and communications.
    /// </summary>
    /// <example>Boys Hostel A</example>
    public string? hostel_name { get; set; }
    
    /// <summary>
    /// Type of the hostel facility (e.g., Boys, Girls, Staff).
    /// Used to categorize hostels based on the target occupants.
    /// </summary>
    /// <example>Boys</example>
    public string? type { get; set; }
}