namespace SIH.ERP.Soap.Models;

public class Admission
{
    public int admission_id { get; set; }
    public string? full_name { get; set; }
    public string? email { get; set; }
    public DateTime? dob { get; set; }
    public string? contact_no { get; set; }
    public string? address { get; set; }
    public int? dept_id { get; set; }
    public int? course_id { get; set; }
    public DateTime? applied_on { get; set; }
    public bool? verified { get; set; }
    public bool? confirmed { get; set; }
    public DateTime? created_at { get; set; }
    public DateTime? updated_at { get; set; }
}