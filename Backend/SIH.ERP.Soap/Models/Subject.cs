namespace SIH.ERP.Soap.Models;

public class Subject
{
    public int subject_code { get; set; }
    public int? course_id { get; set; }
    public string subject_name { get; set; } = string.Empty;
    public int? credits { get; set; }
}

