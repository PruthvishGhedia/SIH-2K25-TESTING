namespace SIH.ERP.Soap.Models;

public class Guardian
{
    public int guardian_id { get; set; }
    public int? student_id { get; set; }
    public string? name { get; set; }
    public string? relationship { get; set; }
    public string? mobile { get; set; }
    public string? address { get; set; }
}