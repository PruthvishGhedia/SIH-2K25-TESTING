namespace SIH.ERP.Soap.Models;

public class Faculty
{
    public int faculty_id { get; set; }
    public string first_name { get; set; } = string.Empty;
    public string last_name { get; set; } = string.Empty;
    public string email { get; set; } = string.Empty;
    public string? phone { get; set; }
    public int? department_id { get; set; }
    public bool? is_active { get; set; }
}

