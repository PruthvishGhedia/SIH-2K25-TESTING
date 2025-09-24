namespace SIH.ERP.Soap.Models;

public class HostelAllocation
{
    public int allocation_id { get; set; }
    public int? student_id { get; set; }
    public int? hostel_id { get; set; }
    public int? room_id { get; set; }
    public DateTime? start_date { get; set; }
    public DateTime? end_date { get; set; }
    public string? status { get; set; }
    public DateTime? created_at { get; set; }
    public DateTime? updated_at { get; set; }
}