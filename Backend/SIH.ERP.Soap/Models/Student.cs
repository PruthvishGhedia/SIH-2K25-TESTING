namespace SIH.ERP.Soap.Models;

public class Student
{
    public int student_id { get; set; }
    public string first_name { get; set; } = string.Empty;
    public string last_name { get; set; } = string.Empty;
    public DateTime dob { get; set; }
    public string email { get; set; } = string.Empty;
    public int? department_id { get; set; }
    public int? course_id { get; set; }
    public int? guardian_id { get; set; }
    public string? admission_date { get; set; }
    public bool? verified { get; set; }
}

