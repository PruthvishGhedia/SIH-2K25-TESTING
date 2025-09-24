namespace SIH.ERP.Soap.Models;

public class Attendance
{
    public int attendance_id { get; set; }
    public int student_id { get; set; }
    public int course_id { get; set; }
    public string date { get; set; } = string.Empty;
    public bool present { get; set; }
}

