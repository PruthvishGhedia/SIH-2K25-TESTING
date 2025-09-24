namespace SIH.ERP.Soap.Models;

public class Exam
{
    public int exam_id { get; set; }
    public int? dept_id { get; set; }
    public int? subject_code { get; set; }
    public DateTime? exam_date { get; set; }
    public string? assessment_type { get; set; }
    public int? max_marks { get; set; }
    public int? created_by { get; set; }
    public DateTime? created_at { get; set; }
    public DateTime? updated_at { get; set; }
}