namespace SIH.ERP.Soap.Models;

public class Enrollment
{
    public int enrollment_id { get; set; }
    public int student_id { get; set; }
    public int course_id { get; set; }
    public string enrollment_date { get; set; } = string.Empty;
    public string? status { get; set; }
}

