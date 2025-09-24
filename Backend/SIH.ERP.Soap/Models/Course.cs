namespace SIH.ERP.Soap.Models;

public class Course
{
    public int course_id { get; set; }
    public int? dept_id { get; set; }
    public string course_name { get; set; } = string.Empty;
    public string? course_code { get; set; }
}

