namespace SIH.ERP.Soap.Models;

/// <summary>
/// Represents a room in a hostel facility in the educational institution.
/// This model stores information about hostel rooms, including capacity and occupancy status.
/// </summary>
public class Room
{
    /// <summary>
    /// Unique identifier for the room record.
    /// This is an auto-generated primary key assigned by the system.
    /// </summary>
    /// <example>1</example>
    public int room_id { get; set; }
    
    /// <summary>
    /// Identifier of the hostel this room belongs to.
    /// References the Hostel table to identify which hostel facility this room is part of.
    /// </summary>
    /// <example>1</example>
    public int? hostel_id { get; set; }
    
    /// <summary>
    /// Room number or identifier within the hostel.
    /// Used for identification and navigation within the hostel facility.
    /// </summary>
    /// <example>A101</example>
    public string? room_no { get; set; }
    
    /// <summary>
    /// Maximum number of occupants the room can accommodate.
    /// Used for allocation planning and occupancy management.
    /// </summary>
    /// <example>3</example>
    public int? capacity { get; set; }
    
    /// <summary>
    /// Current occupancy status of the room (e.g., Available, Occupied, Maintenance).
    /// Used to track room availability for student allocation.
    /// </summary>
    /// <example>Available</example>
    public string? occupancy_status { get; set; }
}