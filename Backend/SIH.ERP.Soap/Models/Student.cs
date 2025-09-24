namespace SIH.ERP.Soap.Models;

public class Student
{
    public int student_id { get; set; }
    public int? user_id { get; set; }
    public int? dept_id { get; set; }
    public int? course_id { get; set; }
    public string? admission_date { get; set; }
    public bool? verified { get; set; }
}

