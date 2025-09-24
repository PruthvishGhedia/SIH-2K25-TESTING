namespace SIH.ERP.Soap.Models;

public class Room
{
    public int room_id { get; set; }
    public int? hostel_id { get; set; }
    public string? room_no { get; set; }
    public int? capacity { get; set; }
    public string? occupancy_status { get; set; }
}